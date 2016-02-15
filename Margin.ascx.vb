#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class Margin
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String
    Dim pubSales As Double = 0
    Dim pubCostofSales As Double = 0
    Dim pubMargin As Double = 0
    Dim pubAnalysis3 As String
    Dim pubExpCode As String
    Dim pubAbsorbed As String
    Dim pubLoadSwitch As String

    Dim pubGridSales As Double = 0
    Dim pubGridWipDebit As Double = 0
    Dim pubGridWipCredit As Double = 0
    Dim pubGridCos As Double = 0
    Dim pubGridMargin As Double = 0
    Dim pubDiscount As Double = 0
    Dim pubCountOrderProcess As Boolean = False
    Dim pubWipCounter As Integer = 0

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
                Label5.Text = pubUser.Trim
                DeletefromAndMargin()

                If pubOrderType.Trim <> "BWAR" And pubOrderType.Trim <> "BEXP" Then
                    ProcessMargin()
                End If
            End If
        End If

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
                            GridView1.DataSourceID = "ObjectDataSource3"
                        Case "013"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource2"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource1"
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
                'clear labels
                Label12.Text = ""
                Label13.Text = ""
                Label14.Text = ""
                Label15.Text = ""
                Label16.Text = ""
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DeletefromAndMargin()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_margin where compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessMargin()
        ProcessMarginOne()
        ProcessMarginTwo()
        InsertIntoMargin2()
        UpdateMarginOne()
        UpdateMarginTwo()
        InsertIntoMargin3()
        If pubOrderType.Trim = "ACAC" Then
            ProcessCurrentAccount()
        End If
    End Sub

    Private Sub ProcessMarginOne()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT analysis3, expense_code, Sum(amount) AS SumOfamount From SES.scheme.prtrm_a " & _
                    "Where (expense_code = 'TRF-MNTSAL' Or expense_code = 'TRF-TRFSAL' Or expense_code= 'TRF-LABSAL' Or expense_code = 'TRF-MATSAL') " & _
                    "And substring(project, 1, 6) = '" & Label2.Text.Trim & "' and remap <> 'y' and " & _
                    "substring(trans_reference,1,4) <> 'proj' and period < '200105' group by analysis3, expense_code"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("SumOfamount")) Then
                        pubSales = mReader("SumOfamount")
                    Else
                        pubSales = 0
                    End If
                    pubAnalysis3 = Trim(mReader("analysis3"))
                    pubExpCode = Trim(mReader("expense_code"))
                    If pubAnalysis3.Trim = "" Then
                        pubAnalysis3 = pubExpCode.Trim
                    End If

                    InsertIntoMargin1()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertIntoMargin1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_margin(orderno,analysis3,sales,wipdebit,wipcredit," & _
                    "costofsales,margin,compname) values('" & Label2.Text.Trim & "','" & pubAnalysis3.Trim & "'," & _
                    pubSales & ",0,0,0,0,'" & pubUser.Trim & "')"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessMarginTwo()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT analysis3, expense_code, Sum(amount) AS SumOfamount From SES.scheme.prtrm_a " & _
                    "Where (expense_code = 'TRF-MNTSAL' Or expense_code = 'TRF-TRFSAL' Or expense_code= 'TRF-LABSAL' " & _
                    "Or expense_code = 'TRF-MATSAL') And substring(project, 1, 6) = '" & Label2.Text.Trim & "' and remap <> 'y' " & _
                    "and period >= 200105 group by analysis3, expense_code"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("SumOfamount")) Then
                        pubSales = mReader("SumOfamount")
                    Else
                        pubSales = 0
                    End If
                    pubAnalysis3 = Trim(mReader("analysis3"))
                    pubExpCode = Trim(mReader("expense_code"))
                    If pubAnalysis3.Trim = "" Then
                        pubAnalysis3 = pubExpCode.Trim
                    End If

                    InsertIntoMargin1()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertIntoMargin2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_margin " & _
            "SELECT substring(project,1,6) as orderno, analysis3, 0 as sales, amount as wipdebit, " & _
                "0 as wipcredit,0 as costofsales,0 as margin,'" & pubUser.Trim & "' as compname " & _
                "FROM SES.scheme.prtrm_a where expense_group <> 'ADV' and expense_group <> 'DSC' " & _
                "and expense_code <> 'TRF-MNTSAL' and expense_code <> 'TRF-TRFSAL' and " & _
                "expense_code <> 'TRF-LABSAL' and expense_code <> 'TRF-MATSAL' and expense_code <> 'TRF-TOCOST' and " & _
                "expense_code <> 'TRF-LABCOS' and expense_code <> 'TRF-DIRCOS' and expense_code <> 'TRF-MATCOS' and " & _
                "substring(project,1,6) = '" & Label2.Text.Trim & "' and remap <> 'y' and substring(glcode,13,4) <> '4022'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateMarginOne()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_margin set wipcredit = wipdebit*-1 where wipdebit < 0 and orderno = '" & _
                Label2.Text.Trim & "' and compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateMarginTwo()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_margin set wipdebit = 0 where wipdebit < 0 and orderno = '" & _
                Label2.Text.Trim & "' and compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoMargin3()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_margin " & _
            "SELECT substring(project,1,6) as orderno, analysis3, 0 as sales, 0 as wipdebit, " & _
            "0 as wipcredit, amount as costofsales, 0 as margin,'" & pubUser.Trim & "' as compname " & _
            "FROM SES.scheme.prtrm_a WHERE (project = '" & Label2.Text.Trim & "' or project = '" & _
             Label2.Text.Trim & "/S' or project = '" & Label2.Text.Trim & "/I') and " & _
            "remap <> 'y' and (expense_code='TRF-TOCOST' or substring(glcode,13,4) = '4001' or " & _
            "substring(glcode,13,4) = '4011' or substring(glcode,13,4) = '4020' or substring(glcode,13,4) = '4010')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessCurrentAccount()
        ProcessCurrentOne()
        ProcessCurrentTwo()
    End Sub

    Private Sub ProcessCurrentOne()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select orderno from SES.scheme.and_margin where compname = '" & pubUser.Trim & _
                    "' and orderno = '" & Label2.Text.Trim & "' and sales <> 0"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then


            Else
                InsertIntoCurrent1()
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertIntoCurrent1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_margin Select order_no, product_group as analysis3, sales*-1 as sales,'0' as wipdebit," & _
                                "'0' as wipcredit,'0' as costofsales,'0' as margin,'" & pubUser.Trim & "' as compname " & _
                                "from SES.scheme.and_backlog where sales <> 0 and comp_name = '" & pubUser.Trim & "' and order_no = '" & _
                                Label2.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessCurrentTwo()
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select analysis3, costofsales from SES.scheme.and_margin where compname = '" & pubUser.Trim & _
                        "' and orderno = '" & Label2.Text.Trim & "' and costofsales <> 0"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubCostofSales = mReader("costofsales")
                    pubAnalysis3 = mReader("analysis3")
                    CheckAbsorbed()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try
    End Sub

    Private Sub CheckAbsorbed()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select project from SES.scheme.prtrm_a where project = '" & Label2.Text.Trim & _
                    "' and analysis3 = '" & pubAnalysis3.Trim & "' and expense_code = 'ABS-BURDEN'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    InsertIntoCurrent2()
                    InsertIntoCurrent3()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetDiscount()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select sum(amount) as sumamount from SES.scheme.prtrm_a where (project = '" & Label2.Text.Trim & "' or project = '" & _
             Label2.Text.Trim & "/S' or project = '" & Label2.Text.Trim & "/I') and expense_group = 'DSC'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("sumamount")) Then
                        pubDiscount = mReader("sumamount")
                    Else
                        pubDiscount = 0
                    End If
                End While
            Else
                pubDiscount = 0
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertIntoCurrent2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_margin(orderno,analysis3,sales,wipdebit,wipcredit," & _
                            "costofsales,margin,compname) values('" & Label2.Text.Trim & "','S95010010'," & _
                            "0,0,0," & pubCostofSales & ",0,'" & pubUser.Trim & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoCurrent3()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_margin set costofsales = 0 where orderno = '" & _
                            Label2.Text.Trim & "' and analysis3 = '" & pubAnalysis3.Trim & "' and compname = '" & _
                            pubUser.Trim & "' and costofsales = " & pubCostofSales

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckLoadSwitch()
        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            If pubDestin = "I" Then
                sSql = "Select order_type, substring(loadaccess_bwar,9,1) as loadaccess_bwar, substring(loadswitch,9,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,9,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If pubDestin = "I" Then
                        If Trim(mReader("order_type")) = "BWAR" Then
                            pubLoadSwitch = mReader("loadaccess_bwar")
                        Else
                            pubLoadSwitch = mReader("loadswitches")
                        End If
                    Else
                        pubLoadSwitch = mReader("loadswitches")
                    End If
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            pubWipCounter = pubWipCounter + 1
            If pubWipCounter = 20 Then
                Panel1.ScrollBars = ScrollBars.Vertical
            End If
            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "analysis3")) Then

                If DataBinder.Eval(e.Row.DataItem, "sales") > 0 Then
                    pubSales = DataBinder.Eval(e.Row.DataItem, "sales") * -1
                Else
                    If DataBinder.Eval(e.Row.DataItem, "sales") < 0 Then
                        pubSales = DataBinder.Eval(e.Row.DataItem, "sales") * -1
                    End If
                End If


                If DataBinder.Eval(e.Row.DataItem, "costofsales") = 0 Or DataBinder.Eval(e.Row.DataItem, "sales") = 0 Then
                    pubMargin = 0
                Else
                    pubMargin = (1 - (DataBinder.Eval(e.Row.DataItem, "costofsales") / pubSales)) * 100
                End If

                'sales
                If DataBinder.Eval(e.Row.DataItem, "sales") = 0 Then
                    e.Row.Cells(1).Text = " "
                Else

                    pubGridSales = pubGridSales + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sales")))

                    If DataBinder.Eval(e.Row.DataItem, "sales") < 0 Then
                        e.Row.Cells(1).Text = Format(DataBinder.Eval(e.Row.DataItem, "sales") * -1, "##,##0.00")
                    Else
                        e.Row.Cells(1).Text = Format(DataBinder.Eval(e.Row.DataItem, "sales"), "##,##0.00")
                    End If

                End If

                'wipdebit
                If DataBinder.Eval(e.Row.DataItem, "wipdebit") = 0 Then
                    e.Row.Cells(2).Text = " "
                Else
                    pubGridWipDebit = pubGridWipDebit + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "wipdebit")))
                    e.Row.Cells(2).Text = Format(DataBinder.Eval(e.Row.DataItem, "wipdebit"), "##,##0.00;(##,##0.00)")
                End If

                'wipcredit
                If DataBinder.Eval(e.Row.DataItem, "wipcredit") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    pubGridWipCredit = pubGridWipCredit + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "wipcredit")))
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "wipcredit"), "##,##0.00;(##,##0.00)")
                End If

                'costofsales
                If DataBinder.Eval(e.Row.DataItem, "costofsales") = 0 Then
                    e.Row.Cells(4).Text = " "
                Else
                    pubGridCos = pubGridCos + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "costofsales")))
                    e.Row.Cells(4).Text = Format(DataBinder.Eval(e.Row.DataItem, "costofsales"), "##,##0.00;(##,##0.00)")
                End If

                If pubMargin = 0 Then
                    e.Row.Cells(5).Text = " "
                Else
                    'pubGridMargin = pubGridMargin + pubMargin
                    e.Row.Cells(5).Text = Format(pubMargin, "##,##0.00;(##,##0.00)")
                End If


            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(1).Text = Format(pubGridSales * -1, "##,##0.00")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(2).Text = Format(pubGridWipDebit, "##,##0.00;(##,##0.00)")
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(3).Text = Format(pubGridWipCredit, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(4).Text = Format(pubGridCos, "##,##0.00;(##,##0.00)")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            GetDiscount()

            If pubGridCos = 0 Or pubGridSales = 0 Then
                pubGridMargin = 0
                Label16.Text = "0.00"
            Else
                pubGridMargin = (1 - (pubGridCos / (pubGridSales * -1))) * 100
                Label16.Text = Format((1 - (pubGridCos / ((pubGridSales * -1) - pubDiscount))) * 100, "##,##0.00;(##,##0.00)")
            End If

            e.Row.Cells(5).Text = Format(pubGridMargin, "##,##0.00;(##,##0.00)")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right


            Label12.Text = Format(pubGridSales * -1, "##,##0.00")
            Label13.Text = Format(pubDiscount, "##,##0.00")
            Label14.Text = Format((pubGridSales * -1) - pubDiscount, "##,##0.00;(##,##0.00)")
            Label15.Text = Format(pubGridCos, "##,##0.00;(##,##0.00)")

        End If

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
