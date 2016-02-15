#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class OrderHeader
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String
    Dim pubLoadSwitch As String
    Dim pubCountOrderProcess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(Page.User.Identity.Name, "\")

        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubUser = UCase(sUserId)
        'pubuser = "HUSSEIN.MOHAMMED"

        CheckConnectionString()

        CheckProcessedOrder()

        If pubCountOrderProcess = False Then
            CheckLoadSwitch()
            If pubLoadSwitch = "1" Then
                CheckOrderNo()
                ClearHeaderLabels()
                DisplayOrderHeader()
            End If
        End If

    End Sub

    Private Sub CheckOrderNo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_no, order_type from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label2.Text = mReader("order_no")
                    pubOrderType = mReader("order_type")
                    Select Case pubOrderType.Trim
                        Case "ACAC"
                            Label4.Text = "Current Account"
                        Case "AGSM"
                            Label4.Text = "GSM"
                        Case "AINS"
                            Label4.Text = "Installation"
                        Case "AREP"
                            Label4.Text = "Repair Workshop"
                        Case "ASIN"
                            Label4.Text = "Supply & Installation"
                        Case "ASUP"
                            Label4.Text = "Supply Only"
                        Case "BCON"
                            Label4.Text = "Consignment"
                        Case "BEXP"
                            Label4.Text = "Expense"
                        Case "BSPR"
                            Label4.Text = "Spare"
                        Case "AMNC"
                            Label4.Text = "Maintenance"
                        Case "BWAR"
                            Label4.Text = "Warranty"
                    End Select
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckConnectionString()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select SES.scheme.and_users.destin, SES.scheme.and_users.user_branch from SES.scheme.and_users WHERE " & _
                    "SES.scheme.and_users.userid='" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubDestin = mReader("destin")
                    pubUserBranch = mReader("user_branch")

                    Select Case mReader("user_branch")
                        Case "012"
                            pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                        Case "013"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                    End Select
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DisplayOrderHeader()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        sSql = ""
        MySqlConn.Open()
        Try
            Select Case pubOrderType.Trim
                Case "BCON", "BEXP", "BSPR"
                    sSql = "SELECT SES.scheme.projects.project_code, SES.scheme.projects.project_status, cost_centre, " & _
                        "SES.scheme.projects.description, SES.scheme.nlcostm.long_description, SES.scheme.projects.customer, " & _
                        "SES.scheme.slcustm.name, SES.scheme.slcustm.address1 AS opaddress1, SES.scheme.slcustm.address2 AS opaddress2, " & _
                        "SES.scheme.slcustm.address3 AS opaddress3, SES.scheme.slcustm.address4 AS opaddress4, " & _
                        "SES.scheme.slcustm.address5, SES.scheme.slcustm.fax, SES.scheme.projects.analysis01 " & _
                        "FROM (SES.scheme.projects INNER JOIN SES.scheme.slcustm ON SES.scheme.projects.customer = SES.scheme.slcustm.customer) " & _
                        "INNER JOIN SES.scheme.nlcostm " & _
                        "ON SES.scheme.projects.cost_centre = SES.scheme.nlcostm.cost_code WHERE SES.scheme.projects.project_code='" & Label2.Text.Trim & "'"

                Case "ACAC", "AGSM", "AINS", "AREP", "ASIN", "ASUP"
                    sSql = "SELECT SES.scheme.projects.project_code, SES.scheme.projects.project_status, SES.scheme.projects.cost_centre, " & _
                        "SES.scheme.nlcostm.long_description, SES.scheme.opheadm.shipping_text, SES.scheme.opheadm.appearance, SES.scheme.projects.customer, " & _
                        "SES.scheme.slcustm.name, SES.scheme.slcustm.address1, SES.scheme.slcustm.address2, SES.scheme.slcustm.address3, SES.scheme.slcustm.address4, " & _
                        "SES.scheme.slcustm.address5, SES.scheme.slcustm.fax, SES.scheme.opheadm.date_entered, SES.scheme.opheadm.date_promised, " & _
                        "SES.scheme.opheadm.customer_order_no, SES.scheme.opheadm.carrier_code, SES.scheme.opheadm.appearance, SES.scheme.opheadm.shipping_text, " & _
                        "SES.scheme.opheadm.address1 as opaddress1, SES.scheme.opheadm.address2 as opaddress2, " & _
                        "SES.scheme.opheadm.address3 as opaddress3, SES.scheme.opheadm.address4 as opaddress4, " & _
                        "SES.scheme.opheadm.invoice_due_date, SES.scheme.opheadm.spare1, SES.scheme.opheadm.order_discount, " & _
                        "SES.scheme.opheadm.responsibility, SES.scheme.projects.project_status, SES.scheme.projects.analysis01 " & _
                        "FROM ((SES.scheme.projects INNER JOIN SES.scheme.slcustm ON SES.scheme.projects.customer = SES.scheme.slcustm.customer) " & _
                        "INNER JOIN SES.scheme.opheadm ON SES.scheme.projects.project_code = SES.scheme.opheadm.order_no) INNER JOIN SES.scheme.nlcostm " & _
                        "ON SES.scheme.projects.cost_centre = SES.scheme.nlcostm.cost_code WHERE SES.scheme.projects.project_code='" & Label2.Text.Trim & "'"
                Case "AMNC"
                    sSql = "SELECT SES.scheme.projects.project_code, SES.scheme.projects.project_type, SES.scheme.projects.cost_centre, " & _
                            "SES.scheme.nlcostm.long_description, SES.scheme.projects.customer, SES.scheme.slcustm.name, SES.scheme.slcustm.address1, " & _
                            "SES.scheme.slcustm.address2, SES.scheme.slcustm.address3, SES.scheme.slcustm.address4, SES.scheme.slcustm.address5, " & _
                            "SES.scheme.slcustm.fax, SES.scheme.opheadm.date_received, SES.scheme.opheadm.date_required, " & _
                            "SES.scheme.opheadm.invoice_due_date, SES.scheme.opheadm.customer_order_no, SES.scheme.opheadm.carrier_code, " & _
                            "SES.scheme.opheadm.appearance, SES.scheme.opheadm.shipping_text, SES.scheme.opheadm.address1 AS opaddress1, " & _
                            "SES.scheme.opheadm.address2 AS opaddress2, SES.scheme.opheadm.address3 AS opaddress3, " & _
                            "SES.scheme.opheadm.spare1, SES.scheme.opheadm.address4 AS opaddress4, SES.scheme.opheadm.address5 AS address5, " & _
                            "SES.scheme.opheadm.order_discount, SES.scheme.opheadm.pricing_date, SES.scheme.projects.project_status, " & _
                            "SES.scheme.projects.analysis01, SES.scheme.projects.analysis04 FROM " & _
                            "((SES.scheme.projects INNER JOIN SES.scheme.slcustm ON SES.scheme.projects.customer = SES.scheme.slcustm.customer) INNER JOIN " & _
                            "SES.scheme.opheadm ON SES.scheme.projects.project_code = SES.scheme.opheadm.order_no) INNER JOIN SES.scheme.nlcostm ON " & _
                            "SES.scheme.projects.cost_centre = SES.scheme.nlcostm.cost_code WHERE SES.scheme.projects.project_code='" & Label2.Text.Trim & "'"
                Case "BWAR"
                    sSql = "SELECT SES.scheme.projects.project_code, SES.scheme.projects.parent_project, SES.scheme.projects.customer, SES.scheme.projects.cost_centre, " & _
                            "SES.scheme.projects.start_date, SES.scheme.projects.end_date, SES.scheme.projects.project_status, " & _
                            "SES.scheme.projects.custname, SES.scheme.projects.sitename, SES.scheme.projects.description as projectname FROM SES.scheme.projects  " & _
                            "WHERE SES.scheme.projects.project_code ='" & Label2.Text.Trim & "'"
            End Select

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Select Case pubOrderType.Trim
                        Case "BWAR"
                            Label5.Text = "Cost Centre"

                            'hide controls
                            Label13.Visible = False
                            Label14.Visible = False

                            Label31.Visible = False
                            Label32.Visible = False
                            Label37.Visible = False
                            Label38.Visible = False
                            Label39.Visible = False
                            Label40.Visible = False
                            Label41.Visible = False
                            Label42.Visible = False
                            Label43.Visible = False
                            Label44.Visible = False
                            Label45.Visible = False
                            Label46.Visible = False
                            Label49.Visible = False
                            Label50.Visible = False

                            Label51.Visible = False
                            Label52.Visible = False
                            Label53.Visible = False
                            Label54.Visible = False

                            'unhide controls
                            Label7.Visible = True
                            Label8.Visible = True
                            Label9.Visible = True
                            Label10.Visible = True

                            Label7.Text = "Warranty No:"
                            Label9.Text = "Order No. :"

                            Label33.Text = "Validity Start :"
                            Label35.Text = "Validty Expiry :"

                            Label8.Text = mReader("project_code")

                            Label6.Text = Trim(mReader("cost_centre"))

                            Label12.Text = Trim(mReader("projectname"))

                            Label17.Text = "Customer : " & Trim(mReader("customer"))
                            Label18.Text = Trim(mReader("custname"))

                            Label20.Text = " "
                            Label21.Text = " "
                            Label22.Text = " "

                            Label24.Text = " "
                            Label26.Text = " "

                            Label8.Text = Label2.Text
                            Label10.Text = Mid(Trim(mReader("parent_project")), 1, 6)

                            If Not IsDBNull(mReader("start_date")) Then
                                Label34.Text = Year(mReader("start_date")).ToString & "/" & Month(mReader("start_date")).ToString & _
                                                "/" & Day(mReader("start_date")).ToString
                            Else
                                Label34.Text = " "
                            End If


                            If Not IsDBNull(mReader("end_date")) Then
                                Label36.Text = Year(mReader("end_date")).ToString & "/" & Month(mReader("end_date")).ToString & _
                                                    "/" & Day(mReader("end_date")).ToString
                            Else
                                Label36.Text = " "
                            End If


                            'Label16.Text = mReader("sitename")

                            Label32.Text = " "
                            Label42.Text = " "

                            Label48.Text = mReader("project_status")

                            Label53.Text = " "

                            Label54.Text = " "

                            Label56.Text = " "

                            GetOrderFromWarranty()


                        Case "AMNC"
                            Label5.Text = "Profit Center"

                            'hide controls
                            Label9.Visible = False
                            Label10.Visible = False
                            Label13.Visible = False
                            Label14.Visible = False
                            Label37.Visible = False
                            Label38.Visible = False
                            Label43.Visible = False
                            Label44.Visible = False

                            Label45.Visible = False
                            Label46.Visible = False

                            'unhide controls
                            Label7.Visible = True
                            Label8.Visible = True

                            Label31.Visible = True
                            Label32.Visible = True
                            Label39.Visible = True
                            Label40.Visible = True
                            Label41.Visible = True
                            Label42.Visible = True
                            Label49.Visible = True
                            Label50.Visible = True

                            Label51.Visible = True
                            Label52.Visible = True
                            Label53.Visible = True
                            Label54.Visible = True


                            Label7.Text = "Date :"

                            Label51.Text = "Salesman :"
                            Label52.Text = "Collector:"

                            Label33.Text = "Validity From :"
                            Label35.Text = "Validty To :"


                            Label8.Text = Year(mReader("date_received")).ToString & "/" & Month(mReader("date_received")).ToString & _
                                                "/" & Day(mReader("date_received")).ToString

                            If Not IsDBNull(mReader("pricing_date")) Then
                                Label34.Text = Year(mReader("pricing_date")).ToString & "/" & Month(mReader("pricing_date")).ToString & _
                                                "/" & Day(mReader("pricing_date")).ToString
                            Else
                                Label34.Text = " "
                            End If


                            If Not IsDBNull(mReader("invoice_due_date")) Then
                                Label36.Text = Year(mReader("invoice_due_date")).ToString & "/" & Month(mReader("invoice_due_date")).ToString & _
                                                    "/" & Day(mReader("invoice_due_date")).ToString
                            Else
                                Label36.Text = " "
                            End If

                            Label6.Text = Trim(mReader("cost_centre")) & " " & mReader("long_description")

                            Label12.Text = Trim(mReader("name"))

                            Label17.Text = "Customer : " & Trim(mReader("customer"))
                            Label18.Text = Trim(mReader("name"))

                            Label20.Text = Trim(mReader("opaddress1"))
                            Label21.Text = Trim(mReader("opaddress2"))
                            Label22.Text = Trim(mReader("opaddress3"))

                            Label24.Text = Trim(mReader("address5"))
                            Label26.Text = Trim(mReader("fax"))

                            Label32.Text = Trim(mReader("customer_order_no"))

                            Label16.Text = mReader("appearance")

                            If Not IsDBNull(mReader("order_discount")) Then
                                If mReader("order_discount") <> 0 Then
                                    Label42.Text = mReader("order_discount") * -1
                                Else
                                    Label42.Text = "0"
                                End If
                            Else
                                Label42.Text = "0"
                            End If

                            GetOrderValue()

                            If Label42.Text.Trim = "0" Then
                                Label42.Text = " "
                            Else
                                Label42.Text = Format(Val(Label42.Text), "##,###.00")
                            End If

                            Label48.Text = mReader("project_status")

                            Label53.Text = mReader("analysis01")

                            Label54.Text = mReader("analysis04")

                            Label56.Text = mReader("shipping_text")


                        Case "ACAC", "AGSM", "AINS", "AREP", "ASIN", "ASUP", "BCON", "BEXP", "BSPR"
                            Label5.Text = "Cost Centre"

                            'hide controls
                            Label7.Visible = False
                            Label8.Visible = False
                            Label9.Visible = False
                            Label10.Visible = False

                            'unhide
                            Label13.Visible = True
                            Label14.Visible = True
                            Label37.Visible = True
                            Label38.Visible = True
                            Label43.Visible = True
                            Label44.Visible = True
                            Label45.Visible = True
                            Label46.Visible = True

                            Label31.Visible = True
                            Label32.Visible = True
                            Label39.Visible = True
                            Label40.Visible = True
                            Label41.Visible = True
                            Label42.Visible = True
                            Label49.Visible = True
                            Label50.Visible = True

                            Label51.Visible = True
                            Label52.Visible = True
                            Label53.Visible = True
                            Label54.Visible = True


                            Label51.Text = "Salesman 1 :"
                            Label52.Text = "2 :"
                            Label33.Text = "Date Entered :"
                            Label35.Text = "Expected Delivery :"


                            If pubOrderType.Trim = "ASIN" Then
                                Label9.Visible = True
                                Label10.Visible = True
                                Label9.Text = "Warranty Order:"
                                GetWarrantyOrder()
                            End If

                            Label6.Text = Trim(mReader("cost_centre")) & " " & mReader("long_description")

                            Label12.Text = Trim(mReader("name"))

                            If pubOrderType.Trim <> "BCON" And pubOrderType.Trim <> "BEXP" And pubOrderType.Trim <> "BSPR" Then
                                Label14.Text = Trim(mReader("responsibility"))
                            End If

                            Label17.Text = "Customer : " & Trim(mReader("customer"))
                            Label18.Text = Trim(mReader("name"))

                            Label20.Text = Trim(mReader("opaddress1"))
                            Label21.Text = Trim(mReader("opaddress2"))
                            Label22.Text = Trim(mReader("opaddress3"))

                            Label24.Text = Trim(mReader("address5"))
                            Label26.Text = Trim(mReader("fax"))

                            If pubOrderType.Trim <> "BCON" And pubOrderType.Trim <> "BEXP" And pubOrderType.Trim <> "BSPR" Then
                                Label32.Text = Trim(mReader("customer_order_no"))

                                Label34.Text = Year(mReader("date_entered")).ToString & "/" & Month(mReader("date_entered")).ToString & _
                                                    "/" & Day(mReader("date_entered")).ToString

                                If Not IsDBNull(mReader("date_promised")) Then
                                    Label36.Text = Year(mReader("date_promised")).ToString & "/" & Month(mReader("date_promised")).ToString & _
                                                        "/" & Day(mReader("date_promised")).ToString
                                Else
                                    Label36.Text = " "
                                End If

                                If Not IsDBNull(mReader("invoice_due_date")) Then
                                    Label38.Text = Year(mReader("invoice_due_date")).ToString & "/" & Month(mReader("invoice_due_date")).ToString & _
                                                        "/" & Day(mReader("invoice_due_date")).ToString
                                Else
                                    Label38.Text = " "
                                End If

                                Label16.Text = mReader("appearance")

                                If Not IsDBNull(mReader("order_discount")) Then
                                    If mReader("order_discount") <> 0 Then
                                        Label42.Text = mReader("order_discount") * -1
                                    Else
                                        Label42.Text = "0"
                                    End If
                                Else
                                    Label42.Text = "0"
                                End If

                                Label44.Text = mReader("carrier_code")

                                If Not IsDBNull(mReader("spare1")) Then
                                    If mReader("spare1") <> 0 Then
                                        Label50.Text = mReader("spare1")
                                    Else
                                        Label50.Text = "0"
                                    End If
                                Else
                                    Label50.Text = "0"
                                End If

                                GetClosingDate()
                                GetOrderValue()

                                If Label42.Text.Trim = "0" Then
                                    Label42.Text = " "
                                Else
                                    Label42.Text = Format(Val(Label42.Text), "##,###.00")
                                End If


                                Label48.Text = mReader("project_status")

                                Label54.Text = " "

                                Label56.Text = mReader("shipping_text")

                            End If

                            Label53.Text = mReader("analysis01")



                    End Select
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetClosingDate()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select actual_date from SES.scheme.prshm where project = '" & Label2.Text.Trim & _
                    "' and status = 'CLOSE'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label46.Text = Year(mReader("actual_date")).ToString & "/" & Month(mReader("actual_date")).ToString & _
                    "/" & Day(mReader("actual_date")).ToString
                End While
            Else
                Label46.Text = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetOrderValue()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select sum(val) as sumtot from SES.scheme.opdetm where order_no = '" & Label2.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("sumtot")) Then
                        Label40.Text = Format(mReader("sumtot") - Val(Label42.Text), "##,###.00")
                    Else
                        Label40.Text = " "
                    End If

                End While
            Else
                Label40.Text = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetWarrantyOrder()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select project_code from SES.scheme.projects where substring(parent_project,1,6) = '" & _
                Label2.Text.Trim & "' and (project_code <> '" & Label2.Text.Trim & "' and project_code <> '" & _
                Label2.Text.Trim & "/S' and project_code <> '" & Label2.Text.Trim & "/I') and project_status <> 'CANCEL'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label10.Text = mReader("project_code")
                End While
            Else
                Label10.Text = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetOrderFromWarranty()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select appearance as appear, shipping_text from SES.scheme.opheadm " & _
                    "where order_no = '" & Label10.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label16.Text = mReader("appear")
                    Label56.Text = mReader("shipping_text")
                End While
            Else
                Label16.Text = " "
                Label56.Text = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckLoadSwitch()
        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select substring(loadswitch,1,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubLoadSwitch = mReader("loadswitches")
                End While
            Else
                pubLoadSwitch = "0"
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ClearHeaderLabels()

        Label6.Text = ""
        Label7.Text = ""
        Label8.Text = ""
        Label10.Text = ""
        Label12.Text = ""
        Label14.Text = ""
        Label17.Text = "Customer :"
        Label16.Text = ""
        Label18.Text = ""
        Label20.Text = ""
        Label21.Text = ""
        Label22.Text = ""

        Label24.Text = ""
        Label28.Text = ""
        Label26.Text = ""
        Label30.Text = ""
        Label32.Text = ""
        Label34.Text = ""

        Label36.Text = ""
        Label38.Text = ""
        Label40.Text = ""
        Label42.Text = ""
        Label44.Text = ""
        Label50.Text = ""

        Label48.Text = ""
        Label46.Text = ""
        Label53.Text = ""
        Label54.Text = ""
        Label56.Text = ""
    End Sub

    Private Sub CheckProcessedOrder()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_process, click from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If mReader("click") = "G" Then
                        pubCountOrderProcess = False
                    Else
                        If mReader("order_process") > 2 Then
                            pubCountOrderProcess = True
                        Else
                            pubCountOrderProcess = False
                        End If
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

End Class
