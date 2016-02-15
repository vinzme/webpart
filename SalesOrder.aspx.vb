#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class SalesOrder
    Inherits System.Web.UI.Page

    Dim pubDomain As String
    Dim pubUser As String

    Dim pubConnStr As String
    Dim pubDestin As String
    Dim pubUserBranch As String
    Dim pubGridClick As Boolean = False
    Dim pubViewOrGrid As String
    Dim pubUserOrderno As Boolean = False
    Dim pubWebMode As Boolean = False
    Dim pubOrderType As String
    Dim pubOrderTypeWarr As String
    Dim pubLoadCounter As Integer
    Dim pubFirstrun As String

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(User.Identity.Name, "\")
        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubDomain = UCase(sDomain)
        pubUser = UCase(sUserId)
        'pubUser = "SALAM.AJAMIEH"

        CheckConnectionString()
        CheckLoadAccess()

        CheckOrderProcess()

        If Page.IsPostBack = False Then
            CheckWebMode()

            If pubWebMode = True Then
                WebPartManager1.DisplayMode = WebPartManager.DesignDisplayMode
                DropDownList1.SelectedIndex = 1
            End If

            WebPartZone1.Visible = False

            Try
                WebPartZone1.WebParts(0).Title = "Order Listing"
            Catch ex As Exception
                Exit Try
            End Try

        End If

    End Sub

    Protected Sub ButtonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
        DropDownList1.SelectedIndex = 0
        Select Case pubUserBranch.Trim
            Case "012"
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource4"
                Else
                    GridView1.DataSourceID = "ObjectDataSource2"
                End If
            Case "013"
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource9"
                Else
                    GridView1.DataSourceID = "ObjectDataSource8"
                End If
            Case Else
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource3"
                Else
                    GridView1.DataSourceID = "ObjectDataSource1"
                End If
        End Select

        If TextBox2.Text.Trim <> "" Then
            Label3.Text = pubUser
            Label4.Text = "%" & UCase(TextBox2.Text.Trim) & "%"
            GridView1.DataBind()
            WebPartZone1.Visible = True

            RadioButtonList1.ClearSelection()
        Else
            Label3.Text = "XXXXXX"
            Label4.Text = "YYYYYY"
        End If
    End Sub

    Protected Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
        DropDownList1.SelectedIndex = 0
        Select Case pubUserBranch.Trim
            Case "012"
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource4"
                Else
                    GridView1.DataSourceID = "ObjectDataSource2"
                End If
            Case "013"
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource9"
                Else
                    GridView1.DataSourceID = "ObjectDataSource8"
                End If
            Case Else
                If pubDestin.Trim = "I" Then
                    GridView1.DataSourceID = "ObjectDataSource3"
                Else
                    GridView1.DataSourceID = "ObjectDataSource1"
                End If
        End Select

        If TextBox2.Text.Trim <> "" Then
            Label3.Text = pubUser
            Label4.Text = "%" & UCase(TextBox2.Text.Trim) & "%"
            GridView1.DataBind()
            WebPartZone1.Visible = True

            RadioButtonList1.ClearSelection()
        Else
            Label3.Text = "XXXXXX"
            Label4.Text = "YYYYYY"
        End If

    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
        DropDownList1.SelectedIndex = 0
        Select Case RadioButtonList1.SelectedValue
            Case "0"
                Label5.Text = "All"
            Case "1"
                Label5.Text = "ASIN"
            Case "2"
                Label5.Text = "ASUP"
            Case "3"
                Label5.Text = "AMNC"
            Case "4"
                Label5.Text = "ACAC"
            Case "5"
                Label5.Text = "BEXP"
            Case "6"
                Label5.Text = "AINS"
            Case "7"
                Label5.Text = "AREP"
            Case "8"
                Label5.Text = "BWAR"
        End Select

        Select Case pubUserBranch.Trim
            Case "012"
                If Label5.Text.Trim = "All" Then
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource12"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource11"
                    End If
                Else
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource17"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource10"
                    End If
                End If
            Case "013"
                If Label5.Text.Trim = "All" Then
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource15"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource14"
                    End If
                Else
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource18"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource13"
                    End If

                End If

            Case Else
                If Label5.Text.Trim = "All" Then
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource7"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource6"
                    End If
                Else
                    If pubDestin.Trim = "I" Then
                        GridView1.DataSourceID = "ObjectDataSource16"
                    Else
                        GridView1.DataSourceID = "ObjectDataSource5"
                    End If

                End If
        End Select

        Label3.Text = pubUser
        GridView1.DataBind()
        WebPartZone1.Visible = True
    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        If pubFirstrun = "0" Then
            ChangeFirstRunToOne()
        End If
        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
        DropDownList1.SelectedIndex = 0
        If TextBox1.Text.Trim <> "" Then
            Label7.Text = TextBox1.Text.Trim
            ResetProcesstoZero()
            AddOnetoProcess()
            CheckUserOrderNo()
            If pubUserOrderno = True Then
                RefreshOrder()
                If pubViewOrGrid = "V" Then
                    pubGridClick = False
                    UpdateUserOrderNo()
                    Response.Redirect("SalesOrder.aspx")
                    WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
                Else
                    pubGridClick = False
                    UpdateUserOrderNo()
                    AddOnetoProcess()
                    WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
                    If pubDestin = "I" And pubOrderType.Trim = "BWAR" Then
                        ChangeWebMode1()
                        SubtractOnetoProcess()
                        PersonalizationAdministration.ResetAllState(PersonalizationScope.User)
                        Response.Redirect("SalesOrder.aspx")
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub ButtonView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If pubFirstrun = "0" Then
            ChangeFirstRunToOne()
        End If
        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
        DropDownList1.SelectedIndex = 0
        If TextBox1.Text.Trim <> "" Then
            Label7.Text = TextBox1.Text.Trim
            ResetProcesstoZero()
            AddOnetoProcess()
            CheckUserOrderNo()
            If pubUserOrderno = True Then
                RefreshOrder()
                If pubViewOrGrid = "V" Then
                    pubGridClick = False
                    UpdateUserOrderNo()
                    Response.Redirect("SalesOrder.aspx")
                    WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
                Else
                    pubGridClick = False
                    UpdateUserOrderNo()
                    AddOnetoProcess()
                    WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
                    If pubDestin = "I" And pubOrderType.Trim = "BWAR" Then
                        ChangeWebMode1()
                        SubtractOnetoProcess()
                        PersonalizationAdministration.ResetAllState(PersonalizationScope.User)
                        Response.Redirect("SalesOrder.aspx")
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PageIndexChanged
        GridView1.SelectedIndex = -1
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        TextBox1.Text = GridView1.SelectedRow.Cells(1).Text.Trim
        pubOrderType = GridView1.SelectedRow.Cells(7).Text.Trim
        pubGridClick = True
        UpdateUserOrderNo()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Select Case DropDownList1.SelectedValue
            Case "0"
                ChangeWebMode1()
                WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode
            Case "1"
                ChangeWebMode2()
                Response.Redirect("SalesOrder.aspx")
            Case "2"
                ChangeWebMode1()
                'ChangeResetModetoOne()
                PersonalizationAdministration.ResetAllState(PersonalizationScope.User)
                Response.Redirect("SalesOrder.aspx")
        End Select

    End Sub

#End Region

#Region "Procedures and Functions"

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

    Private Sub UpdateUserOrderNo()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand
        If pubGridClick = True Then
            cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set order_no = '" & TextBox1.Text.Trim & _
                                    "', click = 'G', order_type ='" & pubOrderType.Trim & "' where userid = '" & pubUser.Trim & "'"
        Else
            cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set order_no = '" & TextBox1.Text.Trim & _
                                    "', click = 'V', order_type ='" & pubOrderType.Trim & "' where userid = '" & _
                                    pubUser.Trim & "'"
        End If

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub RefreshOrder()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select click from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubViewOrGrid = mReader("click")
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckUserOrderNo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        sSql = ""
        MySqlConn.Open()
        Try
            If pubDestin = "I" Then
                sSql = "Select SES.scheme.projects.project_code, SES.scheme.projects.project_type From SES.scheme.projects INNER JOIN SES.scheme.and_users_cc " & _
                        "ON SES.scheme.projects.cost_centre = SES.scheme.and_users_cc.costcenter where " & _
                        "ltrim(SES.scheme.projects.project_code) = '" & TextBox1.Text.Trim & _
                        "/I' and SES.scheme.and_users_cc.userid = '" & pubUser.Trim & "' " & _
                        "union " & _
                        "Select SES.scheme.projects.project_code, SES.scheme.projects.project_type from SES.scheme.projects INNER JOIN SES.scheme.and_users_cc ON " & _
                        "SES.scheme.projects.cost_centre = SES.scheme.and_users_cc.costcenter where " & _
                        "substring(ltrim(SES.scheme.projects.project_code),8,1) <> 'I' and substring(cost_centre,1,2) = '28' " & _
                        "and ltrim(project_code) = '" & TextBox1.Text.Trim & "' and userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select SES.scheme.projects.project_code, SES.scheme.projects.project_type from SES.scheme.projects INNER JOIN SES.scheme.and_users_cc ON " & _
                        "SES.scheme.projects.cost_centre = SES.scheme.and_users_cc.costcenter " & _
                        "where ltrim(SES.scheme.projects.project_code) = '" & TextBox1.Text.Trim & _
                        "' and SES.scheme.and_users_cc.userid = '" & pubUser.Trim & "' " & _
                        "UNION " & _
                        "Select orderno as project_code, project_type from SES.scheme.and_specialorder where " & _
                            "orderno = '" & TextBox1.Text.Trim & "' and userid = '" & pubUser.Trim & "'"
            End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                pubUserOrderno = True
                While mReader.Read()
                    pubOrderType = mReader("project_type")
                End While
            Else
                pubUserOrderno = False
                If pubDestin = "I" Then
                    CheckOrderI()
                End If
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckOrderI()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = "Select SES.scheme.projects.project_code, SES.scheme.projects.project_type From SES.scheme.projects " & _
                        "INNER JOIN SES.scheme.and_users_cc " & _
                        "ON SES.scheme.projects.cost_centre = SES.scheme.and_users_cc.costcenter where " & _
                        "ltrim(SES.scheme.projects.project_code) = '" & TextBox1.Text.Trim & _
                        "/I' and SES.scheme.and_users_cc.userid = '" & pubUser.Trim & "' " & _
                        "union " & _
                        "Select SES.scheme.projects.project_code, SES.scheme.projects.project_type from SES.scheme.projects INNER JOIN SES.scheme.and_users_cc ON " & _
                        "SES.scheme.projects.cost_centre = SES.scheme.and_users_cc.costcenter where " & _
                        "substring(ltrim(SES.scheme.projects.project_code),8,1) <> 'I' " & _
                        "and ltrim(project_code) = '" & TextBox1.Text.Trim & "' and userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                pubUserOrderno = True
                While mReader.Read()
                    pubOrderType = mReader("project_type")
                End While

            Else
                pubUserOrderno = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckWebMode()
        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select web_mode from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If mReader("web_mode") = "0" Then
                        pubWebMode = False
                    Else
                        pubWebMode = True
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try
    End Sub

    Private Sub ChangeWebMode1()
        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set web_mode = '0' where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()
    End Sub

    Private Sub ChangeWebMode2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set web_mode = '1' where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeFirstRunToOne()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set firstrun = '1' where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ResetProcesstoZero()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set order_process = 0 where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub AddOnetoProcess()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set order_process = [order_process]+1 where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub SubtractOnetoProcess()
        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set order_process = [order_process]-1 where userid = '" & _
                    pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()
    End Sub

    Private Sub CheckLoadAccess()

        Dim ConnStr As String
        Dim sSql As String
        Dim i As Integer = 0
        Dim istr As String
        Dim mfirstrun As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = ""
            If pubDestin = "I" Then
                CheckProjectType()
                If pubOrderTypeWarr = "BWAR" Then
                    sSql = "Select order_no, firstrun, loadaccess_bwar as loadaccess from SES.scheme.and_soweb_users where userid = '" & _
                                pubUser.Trim & "'"
                Else
                    sSql = "Select order_no, firstrun, loadaccess from SES.scheme.and_soweb_users where userid = '" & _
                                pubUser.Trim & "'"
                End If

            Else
                sSql = "Select order_no, firstrun, loadaccess from SES.scheme.and_soweb_users where userid = '" & _
                            pubUser.Trim & "'"
            End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader
            pubLoadCounter = 0
            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    mfirstrun = mReader("firstrun")
                    pubFirstrun = mReader("firstrun")
                    Label7.Text = mReader("order_no")
                    For i = 1 To 13
                        istr = Mid(mReader("loadaccess"), i, 1)
                        pubLoadCounter = i
                        Select Case i
                            Case 1
                                Dim WebPartHeader As GenericWebPart
                                WebPartHeader = WebPartManager1.WebParts.Item("OrderHeader1")

                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartHeader)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try

                                Else
                                    WebPartHeader.Title = "Order Header"
                                    WebPartHeader.TitleIconImageUrl = "~/soimages/Header.png"
                                    If mfirstrun = "0" Then
                                        WebPartHeader.ChromeState = PartChromeState.Normal
                                    End If

                                    If WebPartHeader.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If

                                End If
                            Case 2
                                Dim WebPartDetail As GenericWebPart
                                WebPartDetail = WebPartManager1.WebParts.Item("OrderDetail1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartDetail)

                                    Catch ex As Exception
                                        Exit Try
                                    End Try

                                Else
                                    WebPartDetail.Title = "Order Detail"
                                    WebPartDetail.TitleIconImageUrl = "~/soimages/Detail.png"
                                    If mfirstrun = "0" Then
                                        WebPartDetail.ChromeState = PartChromeState.Normal
                                    End If
                                    If WebPartDetail.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 3
                                Dim WebPartPayment As GenericWebPart
                                WebPartPayment = WebPartManager1.WebParts.Item("PaymentSched1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartPayment)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartPayment.Title = "Payment Schedule"
                                    WebPartPayment.TitleIconImageUrl = "~/soimages/Payment.png"
                                    If mfirstrun = "0" Then
                                        WebPartPayment.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartPayment.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 4
                                Dim WebPartAr As GenericWebPart
                                WebPartAr = WebPartManager1.WebParts.Item("AccountsRec1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartAr)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartAr.Title = "Accounts Receivable"
                                    WebPartAr.TitleIconImageUrl = "~/soimages/Ar.png"
                                    If mfirstrun = "0" Then
                                        WebPartAr.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartAr.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 5
                                Dim WebPartAbs As GenericWebPart
                                WebPartAbs = WebPartManager1.WebParts.Item("Absorbed1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartAbs)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartAbs.Title = "Absorbed Burden"
                                    WebPartAbs.TitleIconImageUrl = "~/soimages/Absorbed.png"
                                    If mfirstrun = "0" Then
                                        WebPartAbs.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartAbs.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 6
                                Dim WebPartWip As GenericWebPart
                                WebPartWip = WebPartManager1.WebParts.Item("Workinprogress1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartWip)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartWip.Title = "Work in Progress"
                                    WebPartWip.TitleIconImageUrl = "~/soimages/Wip.png"
                                    If mfirstrun = "0" Then
                                        WebPartWip.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartWip.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 7
                                Dim WebPartCos As GenericWebPart
                                WebPartCos = WebPartManager1.WebParts.Item("CostofSales1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartCos)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartCos.Title = "Cost of Sales"
                                    WebPartCos.TitleIconImageUrl = "~/soimages/Cos.png"
                                    If mfirstrun = "0" Then
                                        WebPartCos.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartCos.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 8
                                Dim WebPartBacklog As GenericWebPart
                                WebPartBacklog = WebPartManager1.WebParts.Item("Backlog1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartBacklog)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartBacklog.Title = "Backlog"
                                    WebPartBacklog.TitleIconImageUrl = "~/soimages/Backlog.png"
                                    If mfirstrun = "0" Then
                                        WebPartBacklog.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartBacklog.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 9
                                Dim WebPartMargin As GenericWebPart
                                WebPartMargin = WebPartManager1.WebParts.Item("Margin1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartMargin)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartMargin.Title = "Margin"
                                    WebPartMargin.TitleIconImageUrl = "~/soimages/Margin.png"
                                    If mfirstrun = "0" Then
                                        WebPartMargin.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartMargin.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 10
                                Dim WebPartAdvance As GenericWebPart
                                WebPartAdvance = WebPartManager1.WebParts.Item("AdvancePayment1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartAdvance)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartAdvance.Title = "Advance Payment"
                                    WebPartAdvance.TitleIconImageUrl = "~/soimages/money.png"
                                    If mfirstrun = "0" Then
                                        WebPartAdvance.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartAdvance.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 11
                                Dim WebPartMaterial As GenericWebPart
                                WebPartMaterial = WebPartManager1.WebParts.Item("Material1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartMaterial)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartMaterial.Title = "Material Status"
                                    WebPartMaterial.TitleIconImageUrl = "~/soimages/Material.png"
                                    If mfirstrun = "0" Then
                                        WebPartMaterial.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartMaterial.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 12
                                Dim WebPartWipSumm As GenericWebPart
                                WebPartWipSumm = WebPartManager1.WebParts.Item("WipSumm1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartWipSumm)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartWipSumm.Title = "WIP Summary"
                                    WebPartWipSumm.TitleIconImageUrl = "~/soimages/WipSum.png"
                                    If mfirstrun = "0" Then
                                        WebPartWipSumm.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartWipSumm.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                            Case 13
                                Dim WebPartCharges As GenericWebPart
                                WebPartCharges = WebPartManager1.WebParts.Item("SOCharges1")
                                If istr = "0" Then
                                    Try
                                        WebPartManager1.CloseWebPart(WebPartCharges)
                                    Catch ex As Exception
                                        Exit Try
                                    End Try
                                Else
                                    WebPartCharges.Title = "Charges after Closing"
                                    WebPartCharges.TitleIconImageUrl = "~/soimages/Charges.png"
                                    If mfirstrun = "0" Then
                                        WebPartCharges.ChromeState = PartChromeState.Minimized
                                    End If
                                    If WebPartCharges.IsClosed = True Then
                                        UpdateSwitchClose()
                                    Else
                                        UpdateSwitchOpen()
                                    End If
                                End If
                        End Select
                    Next

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

    Private Sub CheckProjectType()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_type from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Trim(mReader("order_type")) = "BWAR" Then
                        pubOrderTypeWarr = "BWAR"
                    Else
                        pubOrderTypeWarr = mReader("order_type")
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckOrderProcess()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_no from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Trim(mReader("order_no")) = Label7.Text.Trim Then
                        AddOnetoProcess()
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateSwitchOpen()

        If pubDestin <> "I" Then
            Dim strConnStr As String
            strConnStr = pubConnStr

            Dim MySqlConn As New SqlConnection(strConnStr)

            Dim cmdUpdate As New SqlCommand

            If pubLoadCounter > 1 Then
                cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = substring(loadswitch,1," & pubLoadCounter.ToString.Trim & _
                                        "-1)+'1'+substring(loadswitch," & pubLoadCounter.ToString.Trim & "+1,13-" & pubLoadCounter.ToString.Trim & _
                                        ") Where userid = '" & pubUser.Trim & "'"
            Else
                If pubLoadCounter = 13 Then
                    cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = substring(loadswitch,1,12)+'1' Where userid = '" & pubUser.Trim & "'"
                Else
                    cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = '1'+substring(loadswitch,2,12) Where userid = '" & pubUser.Trim & "'"
                End If
            End If

            cmdUpdate.Connection = MySqlConn
            cmdUpdate.Connection.Open()
            cmdUpdate.ExecuteNonQuery()
            MySqlConn.Close()
        End If
    End Sub

    Private Sub UpdateSwitchClose()

        If pubDestin <> "I" Then

            Dim strConnStr As String
            strConnStr = pubConnStr

            Dim MySqlConn As New SqlConnection(strConnStr)

            Dim cmdUpdate As New SqlCommand

            If pubLoadCounter > 1 Then
                cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = substring(loadswitch,1," & pubLoadCounter.ToString.Trim & _
                                        "-1)+'0'+substring(loadswitch," & pubLoadCounter.ToString.Trim & "+1,13-" & pubLoadCounter.ToString.Trim & _
                                        ") Where userid = '" & pubUser.Trim & "'"
            Else
                If pubLoadCounter = 13 Then
                    cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = substring(loadswitch,1,12)+'0' Where userid = '" & pubUser.Trim & "'"
                Else
                    cmdUpdate.CommandText = "update SES.scheme.and_soweb_users set loadswitch = '0'+substring(loadswitch,2,12) Where userid = '" & pubUser.Trim & "'"
                End If
            End If

            cmdUpdate.Connection = MySqlConn
            cmdUpdate.Connection.Open()
            cmdUpdate.ExecuteNonQuery()
            MySqlConn.Close()

        End If
    End Sub

#End Region

End Class
