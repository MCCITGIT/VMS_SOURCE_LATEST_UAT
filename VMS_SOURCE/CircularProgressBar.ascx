<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CircularProgressBar.ascx.vb" Inherits="CircularProgressBar" %>

<div class="circle-wrap" id="circleWrapDiv" runat="server">
    <div class="hightLightCircle">
        <div class="circle-inner">
            <div class="valueNo"><%= RatingValue %><span class="valuTotalNo">/100</span></div>
            <div class="statusName <%= RatingLabel %>"><%= RatingLabel %></div>
        </div>
    </div>
</div>
