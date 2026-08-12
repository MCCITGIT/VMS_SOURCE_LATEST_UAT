'Imports VMS.Common
'Imports VMS.BusinessFacade
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports VMS.Web

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    'Private userInfo As HIERARCHYUserEntity = New HIERARCHYUserEntity()
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Public Property HideSidebar As Boolean = False
    Dim strscroller As String

#Region "Page Load Events"
    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        HideSidebar = False
        'If Not IsPostBack Then
        'Dim userInfo As VMSUserEntity = New VMSUserEntity()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            lblUserName.Text = userInfo.userFirstNameEntity & " " & userInfo.userLastNameEntity
            lblUserGroup.Text = userInfo.userGroupCodeEntity
            lblCompany.Text = userInfo.userCompanyEntity
            lblRegion.Text = userInfo.userRegionEntity
            lblDepot.Text = userInfo.userBranchEntity
            PopulateMenus()
        Else
            Response.Redirect("~/Login.aspx")
        End If

        'Dim srl As Integer = 0
        'End If

    End Sub
#End Region

#Region "Today Registration"

    Public Sub Today_Reg(ByVal Logincompany As String)

        Dim userDetailsObject As New UserLogin()
        Dim TodayRegSet As DataSet
        TodayRegSet = userDetailsObject.GetTodayReg(Logincompany)
        If (Not (TodayRegSet Is Nothing) AndAlso TodayRegSet.Tables.Count > 0) Then
            If (Not (TodayRegSet.Tables(0) Is Nothing) AndAlso TodayRegSet.Tables(0).Rows.Count > 0) Then
                Dim i As Integer
                strscroller = ""
                For i = 0 To TodayRegSet.Tables(0).Rows.Count - 1
                    strscroller &= (i + 1).ToString & ". Plot " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("bk_plot_display").ToString() & "</font></strong>" & "&nbsp;" & "Layout: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("prj_layout_name").ToString() & "</font></strong>" & "&nbsp;" & "Client: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("clientname").ToString() & "</font></strong>" & "&nbsp;" & "ME: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("usp_initials").ToString() & "</font></strong>" & "&nbsp</br></br>"
                Next
                strscroller = "<marquee id=mar1 style='height:60px;' SCROLLDELAY=300 direction=up onmouseover='this.stop();' onmouseout='this.start();'>" & strscroller & "</marquee>"
                reg_marquee_scroll.InnerHtml = strscroller

            End If
        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub

#End Region

#Region "PopulateMenus"
    Private Function generateInnerTag(dt As DataTable, frmId As Integer, frmNme As String, frmLink As String, frmParentId As Integer) As String
        Dim str As New StringBuilder()

        Dim childCount As Integer = (From chld In dt.AsEnumerable()
                                     Where chld.Field(Of Integer)("fmm_parent_id").Equals(frmId)
                                     Select chld).Count()

        If childCount > 0 Then
            str.Append("<ul class=""collapse list-unstyled"" id=""menu" & frmId.ToString() & """>")

            Dim mainMenu = From menuCode In dt.AsEnumerable()
                           Where menuCode.Field(Of Integer)("fmm_parent_id").Equals(frmId)
                           Select New With {
                           .id = menuCode.Field(Of String)("fmm_id"),
                           .name = menuCode.Field(Of String)("fmm_name"),
                           .link = menuCode.Field(Of String)("fmm_link"),
                           .icon = menuCode.Field(Of String)("fmm_menu_icon"),
                           .parent_id = menuCode.Field(Of Integer)("fmm_parent_id")
                       }

            For Each item In mainMenu
                str.Append("<li>")
                str.Append("<a href=""" & item.link & "?rand=" & DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") & """ title="""">" & item.icon & "" & item.name & "</a>")
                str.Append("</li>")
            Next

            str.Append("</ul>")
        End If

        Return str.ToString()
    End Function

    Private Sub PopulateMenus()
        'Dim userInfo As VMSUserEntity = New VMSUserEntity()
        'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
        'userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Dim userDetailsObject As New UserLogin()
            Dim adminDataSet As DataSet
            adminDataSet = userDetailsObject.LoginUserFormAccess(userInfo.userCompanyEntity, userInfo.userIDEntity, userInfo.userGroupCodeEntity)
            If adminDataSet IsNot Nothing AndAlso adminDataSet.Tables.Count > 0 AndAlso adminDataSet.Tables(0).Rows.Count > 0 Then
                Dim dt As DataTable = Nothing
                dt = adminDataSet.Tables(0)
                Dim mainMenu = From menuCode In dt.AsEnumerable()
                               Where menuCode.Field(Of Integer)("fmm_parent_id") = 0
                               Select New With {
                                   .id = menuCode.Field(Of String)("fmm_id"),
                                   .name = menuCode.Field(Of String)("fmm_name"),
                                   .link = menuCode.Field(Of String)("fmm_link"),
                                   .icon = menuCode.Field(Of String)("fmm_menu_icon"),
                                   .parent_id = menuCode.Field(Of Integer)("fmm_parent_id")
                               }
                Dim strMenu As New StringBuilder()
                strMenu.Append("<ul class=""components"">")
                For Each item In mainMenu

                    If item.link = "#" Then
                        strMenu.Append("<li>")
                        strMenu.Append("<a href=""#menu" & item.id & """ data-toggle=""collapse"" aria-expanded=""false"" class=""dropdown-toggle"" aria-controls=""menu" & item.id & """ title="""">")
                        strMenu.Append("" & item.icon & "" & item.name & "")
                        strMenu.Append("</a>")

                    Dim menuStr As String = generateInnerTag(dt, item.id, item.name, item.link, item.parent_id)
                    strMenu.Append(menuStr)
                        strMenu.Append("</li>")
                    Else
                        strMenu.Append("<li>")
                        strMenu.Append("<a href=""" & item.link & """>")
                        strMenu.Append("<p>" & item.name & "</p>")
                        strMenu.Append("</a>")

                    Dim menuStr As String = generateInnerTag(dt, item.id, item.name, item.link, item.parent_id)
                    strMenu.Append(menuStr)
                        strMenu.Append("</li>")
                    End If
                Next
                strMenu.Append("</ul>")
                _clientScript.Text = strMenu.ToString()
            End If
        'End If
    End Sub

    'Public Sub PopulateMenus()

    '    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '    If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '        userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Dim userDetailsObject As New UserLogin()
    '        Dim adminDataSet As DataSet
    '        adminDataSet = userDetailsObject.LoginUserFormAccess(userInfo.userCompanyEntity, userInfo.userIDEntity, userInfo.userGroupCodeEntity)
    '        If (Not (adminDataSet Is Nothing) AndAlso adminDataSet.Tables.Count > 0) Then
    '            If (Not (adminDataSet.Tables(0) Is Nothing) AndAlso adminDataSet.Tables(0).Rows.Count > 0) Then

    '                Dim strMenu As String

    '                Dim arrayMenuSysAdmin(1000, 1) As String
    '                Dim arrayMenuHOMarketing(1000, 1) As String
    '                Dim arrayMenuDepot(1000, 1) As String
    '                Dim arrayMenuDespatchUnit(1000, 1) As String
    '                Dim arrayMenuToken(1000, 1) As String
    '                Dim arrayMenuQC(1000, 1) As String
    '                Dim arrayMenuVRS(1000, 1) As String

    '                Dim intSrlno1 As Integer
    '                Dim intSrlno2 As Integer
    '                Dim intSrlno3 As Integer
    '                Dim intSrlno4 As Integer
    '                Dim intSrlno5 As Integer
    '                Dim intSrlno6 As Integer
    '                Dim intSrlno7 As Integer

    '                Dim blnSysAdmin As Integer
    '                Dim blnHOMarketing As Integer
    '                Dim blnDepot As Integer
    '                Dim blnDespatch As Integer
    '                Dim blnToken As Integer
    '                Dim blnQC As Integer
    '                Dim blnVRS As Integer

    '                Dim intI As Integer
    '                Dim strSplit() As String

    '                'imgScoreCard.Visible = False
    '                'imgDashBoard.Visible = False

    '                For Each dRow In adminDataSet.Tables(0).Rows
    '                    If Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.SYSADMIN Then


    '                        'If Trim(drRow.Item(2).ToString) = "Dash Board" Then
    '                        'imgDashBoard.Visible = True
    '                        'Else
    '                        intSrlno1 += 1
    '                        arrayMenuSysAdmin(intSrlno1, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                        'End If
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.HOMARKETING Then
    '                        intSrlno2 += 1
    '                        arrayMenuHOMarketing(intSrlno2, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.DEPOT Then
    '                        intSrlno3 += 1
    '                        arrayMenuDepot(intSrlno3, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.UNIT Then
    '                        intSrlno4 += 1
    '                        arrayMenuDespatchUnit(intSrlno4, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.TOKEN Then
    '                        intSrlno5 += 1
    '                        arrayMenuToken(intSrlno5, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.QC Then
    '                        intSrlno6 += 1
    '                        arrayMenuQC(intSrlno6, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.VRS Then
    '                        intSrlno7 += 1
    '                        arrayMenuVRS(intSrlno7, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
    '                    End If

    '                Next

    '                If arrayMenuSysAdmin(1, 1) <> "" Then blnSysAdmin = 1 Else blnSysAdmin = 0
    '                If arrayMenuHOMarketing(1, 1) <> "" Then blnHOMarketing = 1 Else blnHOMarketing = 0
    '                If arrayMenuDepot(1, 1) <> "" Then blnDepot = 1 Else blnDepot = 0
    '                If arrayMenuDespatchUnit(1, 1) <> "" Then blnDespatch = 1 Else blnDespatch = 0
    '                If arrayMenuToken(1, 1) <> "" Then blnToken = 1 Else blnToken = 0
    '                If arrayMenuQC(1, 1) <> "" Then blnQC = 1 Else blnQC = 0
    '                If arrayMenuVRS(1, 1) <> "" Then blnVRS = 1 Else blnVRS = 0

    '                '-- Build Javasript code Dyanamically
    '                strMenu = "<SCRIPT LANGUAGE=javascript>"
    '                strMenu = strMenu & " eDW('<ul class=components id=sidebarNavToggle>');"

    '                '-- System Administration Menu
    '                If arrayMenuSysAdmin(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d1','','<i class=""fa fa-cogs""></i>System Administration','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",1)');"
    '                    For intI = 1 To UBound(arrayMenuSysAdmin)
    '                        If arrayMenuSysAdmin(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuSysAdmin(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d1L1','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If

    '                '*********HOMARKETING Menu
    '                If arrayMenuHOMarketing(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d2','','<i class=""fa fa-home""></i>Distribution & Logistics','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",2)');"
    '                    For intI = 1 To UBound(arrayMenuHOMarketing)
    '                        If arrayMenuHOMarketing(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuHOMarketing(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d2L2','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If

    '                '*********Depot Menu
    '                If arrayMenuDepot(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d3','','<i class=""fa fa-street-view""></i>Depot','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",3)');"
    '                    For intI = 1 To UBound(arrayMenuDepot)
    '                        If arrayMenuDepot(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuDepot(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d3L3','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If

    '                '*********Shipper Menu
    '                If arrayMenuDespatchUnit(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d4','','<i class=""fa fa-universal-access""></i>Despatch Unit','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",4)');"
    '                    For intI = 1 To UBound(arrayMenuDespatchUnit)
    '                        If arrayMenuDespatchUnit(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuDespatchUnit(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d4L4','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If


    '                '*********Token Menu
    '                If arrayMenuToken(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d5','','<i class=""fa fa-tag""></i>Token Requisition','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",5)');"
    '                    For intI = 1 To UBound(arrayMenuToken)
    '                        If arrayMenuToken(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuToken(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d5L5','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If

    '                '*********Qulity Control Menu
    '                If arrayMenuQC(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d6','','<i class=""fa fa-plug""></i>Qulity Control','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & "," & blnQC & ",6)');"
    '                    For intI = 1 To UBound(arrayMenuQC)
    '                        If arrayMenuQC(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuQC(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d6L6','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If

    '                '*********Vendor Reating
    '                '-----Mothar statrt
    '                If arrayMenuVRS(1, 1) <> "" Then
    '                    'strMenu = strMenu & "eDW('<TR align=left>');"
    '                    strMenu = strMenu & "eDW('<li>');"
    '                    strMenu = strMenu & "eMF('e1d7','','<i class=""fa fa-star""></i>Vendor Reating','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & "," & blnQC & "," & blnVRS & ",7)');"
    '                    For intI = 1 To UBound(arrayMenuVRS)
    '                        If arrayMenuVRS(intI, 1) <> "" Then
    '                            strSplit = Split(arrayMenuVRS(intI, 1), "|")
    '                            strMenu = strMenu & "eLK('e1d6L7','i','" & strSplit(0) & "','" & strSplit(1) & "');"
    '                        Else
    '                            Exit For
    '                        End If
    '                    Next
    '                    strMenu = strMenu & "eDE();"
    '                    strMenu = strMenu & "eDW('</li>');"
    '                    'strMenu = strMenu & "eDW('</tR>');"
    '                End If
    '                '-----Mothar end


    '                strMenu = strMenu & "eDW('</ul>');"
    '                strMenu = strMenu & "document.write(eGSTR);"
    '                strMenu = strMenu & "eGSTR='';"
    '                strMenu = strMenu & "if(eMW3C){eF0()};"
    '                strMenu = strMenu & "if(window.pageXOffset==0){eV0=true}else if(eIE){eV1=true};"
    '                strMenu = strMenu & "if(eGB){eF1()}"

    '                '-- Modify below function whenever any menu item is added
    '                strMenu = strMenu & "function fnClose(SysAdmn,HOMarketing,Depot,DespatchUnit,Token,QC,chk)"
    '                strMenu = strMenu & "{"
    '                strMenu = strMenu & " if (SysAdmn==1 && 1!=chk)"
    '                strMenu = strMenu & " eCS('e1d1');"
    '                strMenu = strMenu & " if (HOMarketing==1 && 2!=chk)"
    '                strMenu = strMenu & " eCS('e1d2');"
    '                strMenu = strMenu & " if (Depot==1 && 3!=chk)"
    '                strMenu = strMenu & " eCS('e1d3');"
    '                strMenu = strMenu & " if (DespatchUnit==1 && 4!=chk)"
    '                strMenu = strMenu & " eCS('e1d4');"
    '                strMenu = strMenu & " if (Token==1 && 5!=chk)"
    '                strMenu = strMenu & " eCS('e1d5');"
    '                strMenu = strMenu & " if (QC==1 && 6!=chk)"
    '                strMenu = strMenu & " eCS('e1d6');"
    '                strMenu = strMenu & " if (QC==1 && 7!=chk)"
    '                strMenu = strMenu & " eCS('e1d7');"
    '                strMenu = strMenu & " }"
    '                strMenu = strMenu & "</SCRIPT>"

    '                '-- Assign the Javascript Built code to the Literal Constant
    '                _clientScript.Text = strMenu

    '                'Today_Reg(userInfo.userCompanyEntity)
    '                'Active_Proj(userInfo.userCompanyEntity)
    '                'QuickLinksGetDetails()
    '                'Dim srlint As Integer = 0
    '                'ActionReqGetDetails(srlint)

    '            End If
    '        End If

    '    Else
    '        Response.Redirect("~/Login.aspx")
    '    End If
    'End Sub
#End Region

End Class