'Imports VMS.Common
'Imports VMS.BusinessFacade
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports VMS.Web


Partial Class Home_old
    Inherits System.Web.UI.Page

    Dim drRow As DataRow
    Dim intSrlno1 As Integer
    Public strFlash As String
    Dim arrayQuick(1000, 1) As String
    Public strA, strToDay, strRegDay, strProj As String
    Dim strscroller As String


#Region "Page Load Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            'PopulateMenus()

            Dim srl As Integer = 0
            PopulateActionRequiredList(srl)

            PopulateNews()
            PopulateLastStockUpdateDate()

        End If

    End Sub
#End Region

#Region "Count Indents for Approval."
    Private Function getUnapprovedIndentsCount() As Integer

        Dim indIndentMaster As New IndentMaster()
        Dim dsIndentList As DataSet
        Dim indent_header As New IndentHeaderEntity()
        Dim row_count As Integer

        row_count = 0

        indent_header.IndentDepot = String.Empty
        indent_header.IndentFinYear = GetStandardParameter(Constant.Common.StandardParameter_ProcessYear)
        indent_header.IndentFinMonth = GetStandardParameter(Constant.Common.StandardParameter_ProcessMonth)
        indent_header.IndentStatus = "E"

        Try
            dsIndentList = indIndentMaster.GetIndentCount(indent_header)
            row_count = CType(dsIndentList.Tables(0).Rows(0)(0).ToString, Integer)
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        Return row_count
    End Function
#End Region

#Region "Count Despatch Challans for Approval."
    Private Function getUnapprovedDespatchChallan(ByVal user_unit As String) As Integer

        Dim cmn As New Common()
        Dim ds As DataSet
        Dim unit As String
        Dim fin_year As String
        Dim fin_month As String

        Dim row_count As Integer

        row_count = 0

        unit = user_unit
        fin_year = GetStandardParameter(Constant.Common.StandardParameter_ProcessYear)
        fin_month = GetStandardParameter(Constant.Common.StandardParameter_ProcessMonth)

        Try
            ds = cmn.GetUnapprovedDespatchCount(unit, fin_year, fin_month)
            row_count = CType(ds.Tables(0).Rows(0)(0).ToString, Integer)
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        Return row_count
    End Function
#End Region

#Region "Count Despatch Challans for Approval."
    Private Function getNewDocUploadedCount(ByVal depot As String) As Integer

        Dim cmn As New Common()
        Dim ds As DataSet

        Dim row_count As Integer

        row_count = 0

        Try
            ds = cmn.GetNewDocUploadedCount(depot)
            row_count = CType(ds.Tables(0).Rows(0)(0).ToString, Integer)
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        Return row_count
    End Function
#End Region

#Region "Populate Action Required List"

    Private Sub PopulateActionRequiredList(ByVal srlint As Integer)

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING) Then
            Dim unapproved_indt_count As Integer = getUnapprovedIndentsCount()

            If unapproved_indt_count > 0 Then
                tdActionReq.InnerHtml = "<a href='IndentsList.aspx' >" + (srlint + 1).ToString + ". Unapproved Indents - ( " + unapproved_indt_count.ToString + " )" + "</a><br/>"
                srlint += 1
            End If

            Dim new_docs_count As Integer = getNewDocUploadedCount(userInfo.userBranchEntity)

            If new_docs_count > 0 Then
                tdActionReq.InnerHtml += "<a href='Doc_Upload.aspx' class='chi'>" + (srlint + 1).ToString + ". New Documents - ( " + new_docs_count.ToString + " )" + "</a><br/>"
                srlint += 1
            End If

        ElseIf (userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            Dim unapproved_desp_challan_count As Integer = getUnapprovedDespatchChallan(userInfo.userIDEntity)

            If unapproved_desp_challan_count > 0 Then
                tdActionReq.InnerHtml += "<a href='UnitDespatchPlanList.aspx' class='chi'>" + (srlint + 1).ToString + ". Unapproved Despatch Challans - ( " + unapproved_desp_challan_count.ToString + " )" + "</a><br/>"
                srlint += 1
            End If
        ElseIf (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
            Dim new_docs_count As Integer = getNewDocUploadedCount(userInfo.userBranchEntity)

            If new_docs_count > 0 Then
                tdActionReq.InnerHtml += "<a href='Doc_Upload.aspx' class='chi'>" + (srlint + 1).ToString + ". New Documents (last 15 days) - ( " + new_docs_count.ToString + " )" + "</a><br/>"
                srlint += 1
            End If
        End If

    End Sub

#End Region

#Region "PopulateMenus"

    Public Sub PopulateMenus()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

            Dim userDetailsObject As New UserLogin()
            Dim adminDataSet As DataSet
            adminDataSet = userDetailsObject.LoginUserFormAccess(userInfo.userCompanyEntity, userInfo.userIDEntity, userInfo.userGroupCodeEntity)
            If (Not (adminDataSet Is Nothing) AndAlso adminDataSet.Tables.Count > 0) Then
                If (Not (adminDataSet.Tables(0) Is Nothing) AndAlso adminDataSet.Tables(0).Rows.Count > 0) Then

                    Dim strMenu As String

                    Dim arrayMenuSysAdmin(1000, 1) As String
                    Dim arrayMenuHOMarketing(1000, 1) As String
                    Dim arrayMenuDepot(1000, 1) As String
                    Dim arrayMenuDespatchUnit(1000, 1) As String
                    Dim arrayMenuToken(1000, 1) As String
                    Dim arrayMenuQC(1000, 1) As String
                    Dim arrayMenuVRS(1000, 1) As String

                    Dim intSrlno1 As Integer
                    Dim intSrlno2 As Integer
                    Dim intSrlno3 As Integer
                    Dim intSrlno4 As Integer
                    Dim intSrlno5 As Integer
                    Dim intSrlno6 As Integer
                    Dim intSrlno7 As Integer

                    Dim blnSysAdmin As Integer
                    Dim blnHOMarketing As Integer
                    Dim blnDepot As Integer
                    Dim blnDespatch As Integer
                    Dim blnToken As Integer
                    Dim blnQC As Integer
                    Dim blnVRS As Integer

                    Dim intI As Integer
                    Dim strSplit() As String

                    'imgScoreCard.Visible = False
                    'imgDashBoard.Visible = False

                    For Each dRow In adminDataSet.Tables(0).Rows
                        If Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.SYSADMIN Then


                            'If Trim(drRow.Item(2).ToString) = "Dash Board" Then
                            'imgDashBoard.Visible = True
                            'Else
                            intSrlno1 += 1
                            arrayMenuSysAdmin(intSrlno1, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                            'End If
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.HOMARKETING Then
                            intSrlno2 += 1
                            arrayMenuHOMarketing(intSrlno2, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.DEPOT Then
                            intSrlno3 += 1
                            arrayMenuDepot(intSrlno3, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.UNIT Then
                            intSrlno4 += 1
                            arrayMenuDespatchUnit(intSrlno4, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.TOKEN Then
                            intSrlno5 += 1
                            arrayMenuToken(intSrlno5, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.QC Then
                            intSrlno6 += 1
                            arrayMenuQC(intSrlno6, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        ElseIf Trim(dRow.Item(0).ToString) = Constant.UserFormAccess.VRS Then
                            intSrlno7 += 1
                            arrayMenuVRS(intSrlno7, 1) = Trim(dRow.Item(2).ToString) & "|" & Trim(dRow.Item(3).ToString) & "?rand=" & Server.UrlEncode(Now())
                        End If

                    Next

                    If arrayMenuSysAdmin(1, 1) <> "" Then blnSysAdmin = 1 Else blnSysAdmin = 0
                    If arrayMenuHOMarketing(1, 1) <> "" Then blnHOMarketing = 1 Else blnHOMarketing = 0
                    If arrayMenuDepot(1, 1) <> "" Then blnDepot = 1 Else blnDepot = 0
                    If arrayMenuDespatchUnit(1, 1) <> "" Then blnDespatch = 1 Else blnDespatch = 0
                    If arrayMenuToken(1, 1) <> "" Then blnToken = 1 Else blnToken = 0
                    If arrayMenuQC(1, 1) <> "" Then blnQC = 1 Else blnQC = 0
                    If arrayMenuVRS(1, 1) <> "" Then blnVRS = 1 Else blnVRS = 0

                    '-- Build Javasript code Dyanamically
                    strMenu = "<SCRIPT LANGUAGE=javascript>"
                    strMenu = strMenu & " eDW('<TABLE border=0 cellspacing=2 cellpadding=2 width=100% align=center>');"

                    '-- System Administration Menu
                    If arrayMenuSysAdmin(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d1','','<img src=images/m_sysadmin.bmp  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",1)');"
                        For intI = 1 To UBound(arrayMenuSysAdmin)
                            If arrayMenuSysAdmin(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuSysAdmin(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d1L1','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If

                    '*********HOMARKETING Menu
                    If arrayMenuHOMarketing(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d2','','<img src=images/m_homarketing.jpg  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",2)');"
                        For intI = 1 To UBound(arrayMenuHOMarketing)
                            If arrayMenuHOMarketing(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuHOMarketing(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d2L2','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If

                    '*********Depot Menu
                    If arrayMenuDepot(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d3','','<img src=images/m_Depot.bmp  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",3)');"
                        For intI = 1 To UBound(arrayMenuDepot)
                            If arrayMenuDepot(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuDepot(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d3L3','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If

                    '*********Shipper Menu
                    If arrayMenuDespatchUnit(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d4','','<img src=images/despatchunit_menu_header.jpg  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",4)');"
                        For intI = 1 To UBound(arrayMenuDespatchUnit)
                            If arrayMenuDespatchUnit(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuDespatchUnit(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d4L4','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If


                    '*********Token Menu
                    If arrayMenuToken(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d5','','<img src=images/TokenRequisitionNav.GIF  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & ",5)');"
                        For intI = 1 To UBound(arrayMenuToken)
                            If arrayMenuToken(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuToken(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d5L5','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If

                    '*********Qulity Control Menu
                    If arrayMenuQC(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d6','','<img src=images/quality-control.png  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & "," & blnQC & ",6)');"
                        For intI = 1 To UBound(arrayMenuQC)
                            If arrayMenuQC(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuQC(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d6L6','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If

                    '*********Vendor Reating
                    '-----Mothar statrt
                    If arrayMenuVRS(1, 1) <> "" Then
                        strMenu = strMenu & "eDW('<TR align=left>');"
                        strMenu = strMenu & "eDW('<TD class=menuTreeBox align=left  bgcolor=#e1e1e1 nowrap width=100%>');"
                        strMenu = strMenu & "eMF('e1d7','','<img src=images/vendor-rating.png  border=0>','fnClose(" & blnSysAdmin & "," & blnHOMarketing & "," & blnDepot & "," & blnDespatch & "," & blnToken & "," & blnQC & "," & blnVRS & ",7)');"
                        For intI = 1 To UBound(arrayMenuVRS)
                            If arrayMenuVRS(intI, 1) <> "" Then
                                strSplit = Split(arrayMenuVRS(intI, 1), "|")
                                strMenu = strMenu & "eLK('e1d6L7','i','" & strSplit(0) & "','" & strSplit(1) & "');"
                            Else
                                Exit For
                            End If
                        Next
                        strMenu = strMenu & "eDE();"
                        strMenu = strMenu & "eDW('</TD>');"
                        strMenu = strMenu & "eDW('</tR>');"
                    End If
                    '-----Mothar end


                    strMenu = strMenu & "eDW('</TABLE>');"
                    strMenu = strMenu & "document.write(eGSTR);"
                    strMenu = strMenu & "eGSTR='';"
                    strMenu = strMenu & "if(eMW3C){eF0()};"
                    strMenu = strMenu & "if(window.pageXOffset==0){eV0=true}else if(eIE){eV1=true};"
                    strMenu = strMenu & "if(eGB){eF1()}"

                    '-- Modify below function whenever any menu item is added
                    strMenu = strMenu & "function fnClose(SysAdmn,HOMarketing,Depot,DespatchUnit,Token,QC,chk)"
                    strMenu = strMenu & "{"
                    strMenu = strMenu & " if (SysAdmn==1 && 1!=chk)"
                    strMenu = strMenu & " eCS('e1d1');"
                    strMenu = strMenu & " if (HOMarketing==1 && 2!=chk)"
                    strMenu = strMenu & " eCS('e1d2');"
                    strMenu = strMenu & " if (Depot==1 && 3!=chk)"
                    strMenu = strMenu & " eCS('e1d3');"
                    strMenu = strMenu & " if (DespatchUnit==1 && 4!=chk)"
                    strMenu = strMenu & " eCS('e1d4');"
                    strMenu = strMenu & " if (Token==1 && 5!=chk)"
                    strMenu = strMenu & " eCS('e1d5');"
                    strMenu = strMenu & " if (QC==1 && 6!=chk)"
                    strMenu = strMenu & " eCS('e1d6');"
                    strMenu = strMenu & " if (QC==1 && 7!=chk)"
                    strMenu = strMenu & " eCS('e1d7');"
                    strMenu = strMenu & " }"
                    strMenu = strMenu & "</SCRIPT>"

                    '-- Assign the Javascript Built code to the Literal Constant
                    _clientScript.Text = strMenu

                    'Today_Reg(userInfo.userCompanyEntity)
                    'Active_Proj(userInfo.userCompanyEntity)
                    QuickLinksGetDetails()
                    'Dim srlint As Integer = 0
                    'ActionReqGetDetails(srlint)

                End If
            End If

        Else
            Response.Redirect("~/Login.aspx")
        End If
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

#Region "QuickLinks Get Details"

    Private Sub QuickLinksGetDetails()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        If userInfo.userGroupCodeEntity.Equals("UNIT") Then
            tblComplainRegistrationLink.Visible = True
        Else
            tblComplainRegistrationLink.Visible = False
        End If

        Dim QuickLinkGet As New UserLogin
        Dim QuickLinksList As DataSet

        QuickLinksList = QuickLinkGet.GetQuickLink(userInfo.userCompanyEntity, userInfo.userGroupCodeEntity)
        If (Not (QuickLinksList Is Nothing) AndAlso QuickLinksList.Tables.Count > 0) Then
            If (Not (QuickLinksList.Tables(0) Is Nothing) AndAlso QuickLinksList.Tables(0).Rows.Count > 0) Then

                Dim i As Integer
                strscroller = ""
                For i = 0 To QuickLinksList.Tables(0).Rows.Count - 1
                    'strscroller &= (i + 1).ToString & ". Plot " & "<strong><font color='yellow'> " & ActiveProjectSet.Tables(0).Rows(i)("bk_plot_display").ToString() & "</font></strong>" & "&nbsp;" & "Layout: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("prj_layout_name").ToString() & "</font></strong>" & "&nbsp;" & "Client: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("clientname").ToString() & "</font></strong>" & "&nbsp;" & "ME: " & "<strong><font color='yellow'> " & TodayRegSet.Tables(0).Rows(i)("usp_initials").ToString() & "</font></strong>" & "&nbsp</br></br>"
                    strscroller &= (i + 1).ToString & ". " & "<a href='" + QuickLinksList.Tables(0).Rows(i)("form_name") + "'class='chi'>" + QuickLinksList.Tables(0).Rows(i)("form_desc").ToString() + "</a>" & "&nbsp<br/>"
                Next
                'strscroller = "<marquee id=mar1 style='height:60px;' SCROLLDELAY=300 direction=up onmouseover='this.stop();' onmouseout='this.start();'>" & strscroller & "</marquee>"
                tdQuickLink.InnerHtml = strscroller

                'tdQuickLink.InnerHtml = QuickLinksList.Tables(0).Rows(0)("form_desc")
            End If
        End If

    End Sub

#End Region

    '#Region "Action Required Get Details"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub ActionReqGetDetails(ByVal srlint As Integer)

    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim ActionReqGet As New UserLogin
    '        Dim ActionReqList As DataSet

    '        ActionReqList = ActionReqGet.GetActionReq(userInfo.userCompanyEntity, userInfo.userGroupCodeEntity, userInfo.userIDEntity)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("issuecount") > 0 Then
    '                    tdActionReq.InnerHtml = "<a href='Issue_Management.aspx' class='chi'>" + (srlint + 1).ToString + ". Issues -  ( " + ActionReqList.Tables(0).Rows(0)("issuecount").ToString + " )" + "</a><br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If

    '        ActionReq_statement(srlint)
    '    End Sub

    '#End Region
    '#Region "Action Required Get statement Details"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub ActionReq_statement(ByVal srlint As Integer)

    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim ActionReqGet As New UserLogin
    '        Dim ActionReqList As DataSet

    '        ActionReqList = ActionReqGet.GetActionReq_Statement(userInfo.userCompanyEntity, userInfo.userGroupCodeEntity, userInfo.userIDEntity)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("issuecount") > 0 Then
    '                    tdActionReq.InnerHtml = "<a href='Issue_Management.aspx' class='chi'>" + (srlint + 1).ToString + ". Unread Statements -  ( " + ActionReqList.Tables(0).Rows(0)("issuecount").ToString + " )" + "</a><br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If

    '        PoActionRequired(srlint)
    '    End Sub

    '#End Region

    '#Region "Po Approve action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub PoActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim POobj As New VendorPO
    '        Dim ActionReqList As DataSet
    '        ActionReqList = POobj.PoApproveActionGet(userInfo)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("PoActionCount") > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='Vendor_PO_List.aspx' class='chi'>" + (srlint + 1).ToString + ". Pending Po Approval -  ( " + ActionReqList.Tables(0).Rows(0)("PoActionCount").ToString + " )" + "</a>" + "<br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '        Mtl_Rct_ActionRequired(srlint)

    '    End Sub
    '#End Region




    '#Region "Mtl Rct Approve action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub Mtl_Rct_ActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim MtlListobj As New MtlRct_DC
    '        Dim ActionReqList As DataSet
    '        ActionReqList = MtlListobj.MtlRct_approve_actionGet(userInfo)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("mtlActionCount") > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='Mtl_Rct_List.aspx' class='chi'>" + (srlint + 1).ToString + ". Pending Mtl Receipt Approval -  ( " + ActionReqList.Tables(0).Rows(0)("mtlActionCount").ToString + " )" + "</a>" + "<br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '        Depot_adv_ActionRequired(srlint)
    '    End Sub
    '#End Region

    '#Region "Depot advice approve action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub Depot_adv_ActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim depotAdviceobj As New DepotAdvice
    '        Dim ActionReqList As DataSet
    '        ActionReqList = depotAdviceobj.depot_approve_action_Required_get(userInfo)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("depotActionCount") > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='Depot_Advice_List.aspx' class='chi'>" + (srlint + 1).ToString + ". Pending Depot Advice Approval -  ( " + ActionReqList.Tables(0).Rows(0)("depotActionCount").ToString + " )" + "</a>" + "<br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '        IdttoDesp_ActionRequired(srlint)
    '    End Sub
    '#End Region

    '#Region "Depot advice Consignment approve action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub Depot_adv_cons_ActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim depotAdviceobj As New DepotAdvice
    '        Dim ActionReqList As DataSet
    '        Dim sysmonth As String = Month(Date.Now).ToString
    '        If Len(sysmonth) = 1 Then
    '            sysmonth = "0" + sysmonth

    '        End If

    '        ActionReqList = depotAdviceobj.depot_cons_approve_action_Required_get(userInfo, sysmonth)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows(0)("depotActionCount") > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='Depot_Advice_List.aspx' class='chi'>" + (srlint + 1).ToString + ". Pending Depot Advice Approval -  ( " + ActionReqList.Tables(0).Rows(0)("depotActionCount").ToString + " )" + "</a>" + "</br>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '    End Sub
    '#End Region

    '#Region "Depot Idt to Despatch action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub IdttoDesp_ActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim depotAdviceobj As New DepotAdvice
    '        Dim ActionReqList As DataSet
    '        ActionReqList = depotAdviceobj.ActionRequired_Idt_despatch(userInfo)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows.Count > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='IDT_to_despatch.aspx' class='chi'>" + (srlint + 1).ToString + ". Advice Pending for IDT-toDespatch -  ( " + ActionReqList.Tables(0).Rows.Count.ToString + " )" + "</a>" + "<br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '        IdttoRecv_ActionRequired(srlint)
    '    End Sub
    '#End Region

    '#Region "Depot Idt to Receive action Required"
    '    'needs to be comeented out if any Changes to System Required
    '    Private Sub IdttoRecv_ActionRequired(ByVal srlint As Integer)
    '        Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

    '        Else
    '            Response.Redirect("~/Login.aspx")
    '        End If

    '        Dim depotAdviceobj As New DepotAdvice
    '        Dim ActionReqList As DataSet
    '        ActionReqList = depotAdviceobj.ActionRequired_Idt_Receive(userInfo)
    '        If (Not (ActionReqList Is Nothing) AndAlso ActionReqList.Tables.Count > 0) Then
    '            If (Not (ActionReqList.Tables(0) Is Nothing) AndAlso ActionReqList.Tables(0).Rows.Count > 0) Then
    '                If ActionReqList.Tables(0).Rows.Count > 0 Then
    '                    tdActionReq.InnerHtml &= "<a href='IDT_to_receive.aspx' class='chi'>" + (srlint + 1).ToString + ". Advice Pending for IDT-toReceive -  ( " + ActionReqList.Tables(0).Rows.Count.ToString + " )" + "</a>" + "<br/>"
    '                    srlint += 1
    '                End If

    '            End If
    '        End If
    '    End Sub
    '#End Region

#Region "Flash News"
    'Get Flash News from flash news table
    Public Sub PopulateNews()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

            Dim userDetailsObject As New UserLogin()
            Dim newsDataSet As DataSet
            Dim i As Integer
            newsDataSet = userDetailsObject.GetFalshNews(userInfo.userCompanyEntity, userInfo.userIDEntity)
            If (Not (newsDataSet Is Nothing) AndAlso newsDataSet.Tables.Count > 0) Then
                If (Not (newsDataSet.Tables(0) Is Nothing) AndAlso newsDataSet.Tables(0).Rows.Count > 0) Then
                    strscroller = ""
                    For i = 0 To newsDataSet.Tables(0).Rows.Count - 1
                        strscroller &= newsDataSet.Tables(0).Rows(i)("flash_msg").ToString() & "&nbsp;<br/>"
                    Next
                    strscroller = "<marquee id=mar1 style='height:80px;font-size:10pt' SCROLLDELAY=500 direction=up onmouseover='this.stop();' onmouseout='this.start();'>" & strscroller & "</marquee>"
                    news_marquee_scroll.InnerHtml = strscroller

                End If
            End If
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

#Region "PopulateMenus"

    Public Sub PopulateLastStockUpdateDate()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

            Dim userDetailsObject As New UserLogin()
            Dim adminDataSet As DataSet
            Try
                adminDataSet = userDetailsObject.GetLastStockUpdateDate()
                If (adminDataSet.Tables(0).Rows.Count > 0) Then
                    lblLastStockUpdateDate.Text = adminDataSet.Tables(0).Rows(0)(0).ToString()
                End If
            Catch ex As Exception
                Dim returnUrl As String = "~/ExceptionPage.aspx"
                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                Server.Transfer(returnUrl)
            End Try

        End If
    End Sub
#End Region

#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As String = String.Empty

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = dsStandardParameter.Tables(0).Rows(0)("param_char_value")
                Else
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

        Return result

    End Function

#End Region

End Class
