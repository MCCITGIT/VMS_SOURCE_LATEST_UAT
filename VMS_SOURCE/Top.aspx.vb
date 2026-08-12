Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes

Partial Class Top
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim userInfo As VMSUserEntity = New VMSUserEntity()
            If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
                userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
                tdCompany.InnerText = userInfo.userCompanyEntity
                tdRegion.InnerText = userInfo.userRegionEntity
                tdBranch.InnerText = userInfo.userBranchEntity
                tdUid.InnerText = userInfo.userIDEntity
                tdDept.InnerText = userInfo.userDepartmentEntity
            End If

        End If
    End Sub
End Class
