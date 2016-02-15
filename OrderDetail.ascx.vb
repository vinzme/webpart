#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class OrderDetails
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String

    Dim pubOrderTotal As Integer
    Dim pubGridCounter As Integer = 0
    Dim pubLoadSwitch As String
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
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the backlogTotal to the running total variables
            pubWipCounter = pubWipCounter + 1
            If pubWipCounter = 20 Then
                Panel1.ScrollBars = ScrollBars.Vertical

                'Panel1.Width = 99%
                'GridView1.Width = 99%
            End If

            If Mid(DataBinder.Eval(e.Row.DataItem, "long_description").ToString.Trim, 1, 5) = "=====" Then
                e.Row.Cells(1).Text = "===================="
            End If

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "val")) Then
                If Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "val")) = 0 Then
                    e.Row.Cells(0).Text = " "
                    e.Row.Cells(2).Text = " "
                    e.Row.Cells(3).Text = " "
                    e.Row.Cells(4).Text = " "
                    e.Row.Cells(5).Text = " "
                Else
                    pubGridCounter = pubGridCounter + 1
                    If e.Row.Cells(1).Text.Trim = "DISCOUNT" Then
                        e.Row.Cells(0).Text = " "
                        e.Row.Cells(2).Text = " "
                        e.Row.Cells(3).Text = " "
                        e.Row.Cells(5).Text = " "
                    Else
                        e.Row.Cells(0).Text = pubGridCounter.ToString
                        e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "net_price"), "##,##0;(##,##0)")
                    End If

                    e.Row.Cells(4).Text = Format(DataBinder.Eval(e.Row.DataItem, "val"), "##,##0;(##,##0)")

                    pubOrderTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "val"))
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(4).Text = Format(pubOrderTotal, "##,##0;(##,##0)")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).Font.Size = 8

        End If

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
                sSql = "Select order_type, substring(loadaccess_bwar,2,1) as loadaccess_bwar, substring(loadswitch,2,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,2,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
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
