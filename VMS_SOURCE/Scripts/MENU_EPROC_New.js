/*Script copyrighted 2002, Andrew McCafferty, All rights reserved.*/
var eCO = '', eFN; eFN = document.location;
var eV2 = true, emtWin, eGSTR = "", eV3 = new Array(), eV4 = 0, eV0 = false, eV1 = false, eV5 = 0, eGI = new Array(), eV6 = "", eV7, eV8;
var eAGT = navigator.userAgent.toLowerCase(), ePI = parseInt(navigator.appVersion),
eIE = ((eAGT.indexOf('msie') != -1) && (eAGT.indexOf('opera') == -1)), eNN = (navigator.appName == 'Netscape'),
eMAC = (eAGT.indexOf('mac') != -1),
eW3C = ((eIE && ePI >= 4) || (eNN && ePI >= 5)), eMW3C = ((eIE && ePI >= 4) || (eNN && ePI >= 5)),
eGB = !eMW3C, eIE50 = (eAGT.indexOf('msie 5.0') != -1), eDD = !((eNN && eAGT.indexOf('6/6.0') != -1) || (eIE && eMAC));
if (document.images) { var eV44 = new Image(6, 4); eV44.src = "images/b_dot.gif"; };
function eMF(eV9, eV10, eV11, eV12) { eGI[eV11] = eV9; eV11 = eF2(eV11, eV10); if (eMW3C) { var t1, t2, t3; if (eF3(eV10)) { t1 = "onMouseOut=eF4('" + eV9 + "','" + eV10 + "',event) onMouseOver=eF5('" + eV9 + "','" + eV10 + "',event)"; t2 = "display:None;position:absolute"; t3 = "" } else { t1 = ""; t2 = "display:None"; t3 = "margin-left:1px" }; eF7(eV9, eV10, eV11, eV12); eDW("<ul class='collapse list-unstyled' id='" + eV9 + "' data-parent='#sidebarNavToggle'><li>") } else if (eGB) { if (eF8(eCO, eV9)) { eV4 = 0; eV5 = 18 } else { eV4 = 1 } } }
function eSF(eV13, eV10, eV11, eV12) { eGI[eV11] = eV13; eV11 = eF2(eV11, eV10); if (eMW3C) { var t1, t2, t3; if (eF3(eV10)) { t1 = "onMouseOut=eF4('" + eV13 + "','" + eV10 + "',event) onMouseOver=eF5('" + eV13 + "','" + eV10 + "',event)"; t2 = "display:None;position:absolute"; t3 = "" } else { t1 = ""; t2 = "display:None"; t3 = "margin-left:9" }; eDW("<div id='c" + eV13 + "' " + t1 + " class='subFolderBox" + eF6(eV10) + "' style='" + t3 + "'>"); eF9(eV13, eV10, eV11, eV12); eDW("<ul class='collapse list-unstyled' id='" + eV13 + "' data-parent='#sidebarNavToggle'><li>"); } else if (eGB) { if (eF8(eCO, eGF(eV13))) { eDW("<p id='c" + eV13 + "' class='subFolderBox" + eF6(eV10) + "' style='margin-left: " + eV5 + "px'>"); eF9(eV13, eV10, eV11, eV12); eDW('</p>') }; if (eF8(eCO, eV13)) { eV4 = 0; eV5 = eV5 + 9 } else { eV4 = eV4 + 1 } }; }
//function eLK(eV14,eV10,eV11,eV15){eV14='b'+eV14;eGI[eV11]=eV14;eV11=eF2(eV11,eV10);var eV16;if(eV4==0){eV16 =eF10(eV10,eV15);if(eMW3C){var t1,t2;if(eF3(eV10)){t1="";t2="onMouseOver=eF5('"+eV14 +"','',event) id='"+eV14 +"'"}else{t1="margin-left:20px";t2=""};eDW("<div style='HEIGHT: 20px' id='c"+eV14 +"' "+t2+" class='linkBox"+eF6(eV10)+"' style='"+t1+"'>");eF11(eV14,eV10,eV11,eV16,eV15);eDW("</div>")}else if(eGB){eDW("<p id='c"+eV14 +"' class='linkBox"+eF6(eV10)+"' style='margin-left:"+eV5 +"px'>");eF11(eV14,eV10,eV11,eV16,eV15);eDW('</p>')};}}
function eLK(eV14, eV10, eV11, eV15) { eV14 = 'b' + eV14; eGI[eV11] = eV14; eV11 = eF2(eV11, eV10); var eV16; if (eV4 == 0) { eV16 = eF10(eV10, eV15); if (eMW3C) { var t1, t2; if (eF3(eV10)) { t1 = ""; t2 = "onMouseOver=eF5('" + eV14 + "','',event) id='" + eV14 + "'" } else { }; eDW(""); eF11(eV14, eV10, eV11, eV16, eV15); eDW("</div>") } else if (eGB) { eDW(""); eF11(eV14, eV10, eV11, eV16, eV15); }; } }
function eCOL(eV17) { var t; if (eV17 == '') { t = '' } else { t = 'width=' + eV17 }; eDW("<TD class='menuTreeBox' " + t + ' ' + "valign='top'>"); }
function eDE() { if (eMW3C) { eDW("</li></ul></div>") } else if (eGB) { if (eV4 == 0) { eV5 = eV5 - 9 } else { eV4 = eV4 - 1 } }; }
function eF12(eV18, eV10, eV11, eV12) { if (eGB && eF8(eCO, eV18)) { eV3[eV18] = true }; var t1; if (eF3(eV10)) { t1 = "//" } else { t1 = "" }; eDW(eF14(eV18, eV11, "{" + eV12 + "}", t1, "dropdown-toggle" + eF6(eV10), eV11) + eV11 + "</a>") }
function eF9(eV19, eV10, eV11, eV12) { if (eGB && eF8(eCO, eV19)) { eV3[eV19] = true }; var t1; if (eF3(eV10)) { t1 = "//" } else { t1 = "eF13('" + eV19 + "','" + eV10 + "')" }; eDW(eF14(eV19, eV11, "if(!eV3['" + eV19 + "']){" + eV12 + "}", t1, "subFolderLine" + eF6(eV10), eV11) + eV11 + "</a>") }
function eF11(eV14, eV10, eV11, eV16, eV15) { var eV20, eV21 = 'eF15(0,0);'; if (eF16(eV10, 'j')) { eV21 = '' }; if (!eF16(eV10, 'nl')) { eV20 = 'subMenuATag'; eDW(eF14(eV14, eV11, "{" + eV21 + "" + eV16 + ";return false}", "//", eV20 + eF6(eV10), eV15)) }; if (!eF16(eV10, 'nm')) { eDW("<i class='fa fa-dot-circle'></i>") }; eDW(eV11); if (!eF16(eV10, 'nl')) { eDW("</a>") }; }
function eF14(eV22, eV11, ocjs, hrck, cn, eV23) {
    //alert("<a id='a"+eV22+"' onclick=\""+ocjs+"\" href=\"javascript:"+hrck+"\" onMouseOver=\""+eF17(cn,eV23)+";return true\" onMouseOut=\""+eF18(cn)+";return true\" class='"+cn+"'>")
    return "<a id='a" + eV22 + "' data-toggle=\"collapse\" aria-expanded=\"false\" data-parent=\"#sidebarNavToggle\" onclick=\"" + ocjs + "\" href='#" + eV22 + "' onMouseOver=\"" + eF17(cn, eV23) + ";return true\" onMouseOut=\"" + eF18(cn) + ";return true\" class='" + cn + "'>"
}
function eF17(cn, eV24) { return "window.status='" + eF19(eV24) + "';if(eW3C){this.className='" + cn + "MouseOver'}" }
function eF18(cn) { return "window.status='';if(eW3C){this.className='" + cn + "'}" }
function eF20(eV22, gif, h, w) { return "<IMG border='0' src='" + gif + "' width='" + w + "' height='" + h + "' id='i" + eV22 + "'>" }
function eF13(eV14, eV10) { if (!eF3(eV10)) { eUDC(eV14) }; eF21(eV14) }
function eF21(eV14) {

    if (eMW3C) {
        if (eV3[eV14]) {
            eV3[eV14] = false;
            eGO(eV14).style.display = "none"
        }
        else {
            eV3[eV14] = true;
            if (eGO(eV14) != null)
                eGO(eV14).style.display = "";
        }
    }
    else if (eGB) {
        eF22();
        window.location.href = eFN
    };
}

function eF7(eV9, eV10, eV11, js) { if (eF16(eV10, 'nf')) { js = eF10(eV10, js); eF11(eV9, eV10, eV11, js, eV11) } else { eF12(eV9, eV10, eV11, js) } }
function eF23(eV18) { return eV18 != "out" && eV18.indexOf("d", eV18.indexOf("d") + 1) == -1 }
function eGO(eV13) { if (document.getElementById) { return eval(document.getElementById(eV13)) } else { return eval(document.all(eV13)) } }
function eOS(eV25) { if (eGB) { if (!eV3[eV25]) { eF21(eV25) } } else { var start = 0; start = eV25.indexOf("d", start) + 1; start = eV25.indexOf("d", start) + 1; while (start > 0) { if (!eV3[eV25.substring(0, start - 1)]) { eF21(eV25.substring(0, start - 1)) }; start = eV25.indexOf("d", start) + 1 }; if (!eV3[eV25]) { eF21(eV25) } } }
function eCS(eV25) { if (eV3[eV25]) { eF21(eV25) } }
function eGF(eV26) { if (eV26.substring(0, 1) == 'b') { eV26 = eV26.slice(1) }; var j, p = eV26.lastIndexOf('d'); j = eV26.substring(0, p).lastIndexOf('d'); if (j == -1) { return "" } else { return eV26.substring(0, p) } }
function eUDC(eV14) { if (eGB || eV2) { if (eF8(eCO, eV14)) { eF24(eV14) } else { eF25(eV14) }; eCO = eF26('test3') } }
function eDW(str) { eGSTR += str }
function eF10(eF27, eV15) { if (!eF16(eF27, 't')) { eV15 = cnv(eV15, eF27) }; if (eF16(eF27, 'i')) { return eF28(eV15) } else if (eF16(eF27, 'r')) { return eF29(eV15) } else if (eF16(eF27, 'e')) { return eF30(eV15) } else if (eF16(eF27, 'p')) { return eF31(eV15) } else if (eF16(eF27, 'j')) { return eV15 } }
function eF32(eV27, bool) { if (bool) { if (eV3[eV27]) { return "" } else { return "" } } else { if (eV3[eV27]) { return "" } else { return "" } } }
function eF28(eV28) { return "window.location.href = '" + eV28 + "'" }
function eF29(eV28) { if (parent.emtcf) { return "parent.emtcf.location.href = '" + eV28 + "'" } else { return eF30(eV28) } }
function eF31(eV28) { return "parent.location.href = '" + eV28 + "'" }
function eF30(eV28) { return "emtWin=window.open('" + eV28 + "')" }
function eF16(k, b) { if (k.indexOf(b) > -1) { return true }; return false }
function eF19(eV11) { if (eF16(eV11, "'") || eF16(eV11, "</")) { return "" }; return eV11 }
function eF33(eV29) { window.status = eV29; return true }
function eF25(eV30) { var nc = '', start = 0, end; end = eCO.indexOf('x', start); while (end > 0) { if (!eF8(eV30, eCO.substring(start, end))) { nc = nc + eCO.substring(start, end) + 'x' }; start = end + 1; end = eCO.indexOf('x', start) }; eF34('test3', nc + eV30 + 'x') }
function eF24(eV30) { var nc = '', start = 0, end; end = eCO.indexOf('x', start); while (end > 0) { if (!eF8(eCO.substring(start, end + 1), eV30)) { nc = nc + eCO.substring(start, end) + 'x' }; start = end + 1; end = eCO.indexOf('x', start) }; if (eGF(eV30) != '') { nc = nc + eGF(eV30) + 'x' }; if (nc == '') { nc = 'none' }; eF34('test3', nc) }
function eF8(eV31, eV27) { if (eF16(eV31, eV27 + 'd') || eF16(eV31, eV27 + 'x')) { return true } else { return false } }
function eF0() {
    var t, start = 0, end; end = eCO.indexOf('x', start);
    /*if(eW3C)
    {
        t=eGO("et1d1").id
    };*/

    while (end > 0) { eOS(eCO.substring(start, end)); start = end + 1; end = eCO.indexOf('x', start) };
}
function eF26(eV32) { var cs = document.cookie, s = -1, m, e; while (s < cs.length) { m = cs.indexOf('=', s); e = cs.indexOf(';', m); if (e == -1) { e = cs.length }; if (cs.substring(s + 1, m) == eV32) { return cs.substring(m + 1, e) }; s = cs.indexOf(';', s + 1); if (s == -1) { s = cs.length }; s = s + 1 }; return '0' }
function eF34(eV32, eV33) { document.cookie = eV32 + '=' + eV33 + ';path=/' }
function eF22() { var t = 0, l = 0; if (eV0) { l = window.pageXOffset; t = window.pageYOffset } else if (eV1) { t = document.body.scrollTop; y = document.body.scrollLeft }; eF15(l, t) }
function eF15(l, t) { eF34('test3Left', l); eF34('test3Top', t) }
function eF1() { var l, t; l = eF26('test3Left'); t = eF26('test3Top'); if (eV0) { window.scroll(l, t) } else if (eV1) { document.body.scrollLeft = l; document.body.scrollTop = t } }
function eF35() { eF34('test3', 't');; if (eF26('test3') == 't') { return true }; return false }
function eF6(eV10) { var t1; t1 = eV10.indexOf('cn'); if (t1 > -1) { return eV10.substring(t1 + 2, t1 + 4) } else { return '' } }
function eF2(eV11, eV10) { if (eF16(eV10, 'nb')) { eV11 = "<nobr>" + eV11 + "</nobr>" }; return eV11 }
function eOpenPage(ln) { eOS(eGF(eGI[ln])); }
function eClosePage(ln) { eCS(eGF(eGI[ln])); }
function eOpen(ln) { eOS(eGI[ln]); }
function eClose(ln) { eCS(eGI[ln]); }
function eSwap(ln, att, val, aic) { if (eMW3C) { var t = eGO(aic + eGI[ln]); eval("t." + att + "= '" + val + "'") } }
function eSwapBox(ln, att, val) { eSwap(ln, att, val, "c") }
function eSwapImage(ln, att, val) { eSwap(ln, att, val, "i") }
function eSwapLine(ln, att, val) { eSwap(ln, att, val, "a") }
function eF3(eV10) { if (eMW3C && eDD && eF16(eV10, "dd")) { return true }; return false }
function eF36(eV9, eV10) { var eV34, eV35, eV36, eV37, eV38, eV39 = false, eV40 = false, eV41 = 0; if (eF16(eV10, "to")) { eV39 = true }; if (eF16(eV10, "le")) { eV40 = true }; eV34 = eV10.indexOf('.'); if (eV34 == -1) { eV41 = 1 } else { eV41 = eV10.substring(eV34, eV34 + 3) }; eV37 = eGO("c" + eV9); eV38 = eGO(eV9); for (var i = 0; i < 4; i = i + 1) { eV36 = eF37(eV37); eV35 = eF38(eV37); if (eV39) { eV35 = eV35 - eV38.offsetHeight; if (eV41 == 1) { eV35 = eV35 + eV37.offsetHeight } } else if (!eV39) { if (eV41 != 1) { eV35 = eV35 + eV37.offsetHeight } } if (!eV40) { eV36 = eV36 + (eV37.offsetWidth * eV41) } else if (eV40) { eV36 = eV36 - (eV38.offsetWidth * eV41) }; if (eIE50) { eV36 = eV36 - 6 }; eV38.style.left = eV36 + "px"; eV38.style.top = eV35 + "px" } }
function eF38(eV37) { var t = 0; while (eV37) { if (eV37.style.position == 'absolute') { return t }; t = t + eV37.offsetTop; eV37 = eV37.offsetParent }; return t }
function eF37(eV37) { var t = 0; while (eV37) { if (eV37.style.position == 'absolute') { return t }; t = t + eV37.offsetLeft; eV37 = eV37.offsetParent } return t }
function eF5(eV9, eV10, evt) { if (window.event) { window.event.cancelBubble = true } else { evt.cancelBubble = true }; if (!eF16(eV10, 'nf')) { if (eV8) { clearTimeout(eV8) }; if (eV7) { clearTimeout(eV7) }; eV7 = setTimeout("eF39('" + eV9 + "','" + eV10 + "')", 400) } }
function eF4(eV9, eV10, evt) { if (window.event) { window.event.cancelBubble = true } else { evt.cancelBubble = true }; if (!eF16(eV10, 'nf')) { if (eV7) { clearTimeout(eV7) }; eV7 = setTimeout("eF40()", 400) } }
function eF39(eV9, eV10) { if (eV9.substring(0, 1) == 'b') { eV9 = eGF(eV9.slice(1)); while (!eF16(eV9, eV6)) { eF21(eV6); eV6 = eGF(eV6); } } else if (eV6 == "") { eF21(eV9); eF36(eV9, eV10); eV6 = eV9; } else if (eF16(eV9, eV6 + 'd')) { eF21(eV9); eF36(eV9, eV10); eV6 = eV9; } else if (eF16(eV6, eV9 + 'd')) { while (eF16(eV6, eV9 + 'd')) { eF21(eV6); eV6 = eGF(eV6) } } else { eF21(eV6); eV6 = eGF(eV6); while (!eF16(eV9, eV6 + 'd')) { eF21(eV6); eV6 = eGF(eV6); } if (!eV3[eV9]) { eF21(eV9); eF36(eV9, eV10); eV6 = eV9 } } }
function eF40() { eV8 = setTimeout("timeOut()", 400) }
function timeOut() { while (eV6 != "") { eF21(eV6); eV6 = eGF(eV6); } }
function cnv(ju, k) { return ju }
if (eF26('test3') != '0') { if (eGB || eV2) { eCO = eF26('test3') } };
if (eV2 && !eF35()) { eV2 = false };
if (eGB && !eF35()) { alert('For the tree menu to work properly, you must turn on cookies') };
if (eGB || eV2) { eF34('test3', eCO) };

/*eDW("<TABLE border='0' cellspacing='2' cellpadding='2' width='85%' align='center'>");
eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};

eMF("e1d1","","<img src='images/h_security_down.gif'  border='0'>","eCS('e1d2');eCS('e1d3');eCS('e1d4');eCS('e1d5');eCS('e1d6');");
eLK("e1d1L1","i","User Group","l_user_group.asp");
eLK("e1d1L1","i","User Type","l_user_type.asp");
eLK("e1d1L1","i","User Profile Master","l_user_profile.asp");
eLK("e1d1L1","i","Form Menu Management","l_form_menu.asp");
eLK("e1d1L1","i","Assign Form Privilege","m_assign_forms.asp");


eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};

eMF("e1d2","","<img src='images/h_admin_down.gif'  border='0'>","eCS('e1d1');eCS('e1d3');eCS('e1d4');eCS('e1d5');eCS('e1d6');");
eLK("e1d1L1","i","Reliance Telecom Circle","rel_tel_circle.asp");
eLK("e1d1L1","i","CAF-Product - Master","l_caf_product.asp");
eLK("e1d1L1","i","NIMS-Product - Master","l_product.asp");
eLK("e1d1L1","i","CAF-NIMS-Product- Link","m_caf_nims_link_master.asp");
eLK("e1d1L1","i","CAF-Master Details","l_caf_master_input.asp");
eLK("e1d1L1","i","Number Type Master","l_num_type.asp");
eLK("e1d1L1","i","Number Lifecycle Status","l_num_life.asp");
eLK("e1d1L1","i","SDCA Master","l_sdca.asp");
eLK("e1d1L1","i","City / Town Master","l_city_town.asp");
eLK("e1d1L1","i","Zone Master","l_zone.asp");
eLK("e1d1L1","i","BAN - Master","l_ban.asp");
eLK("e1d1L1","i","GIS - UID Master","l_gis_uid.asp");
eLK("e1d1L1","i","Customer Type Master","l_customer_type.asp");
eLK("e1d1L1","i","GIS UID Upload","l_upload_gisuid.asp");
eLK("e1d1L1","i","Reliance-Company Master","l_company_type.asp");
eLK("e1d1L1","i","WLN Number Master Plan","l_wireline_num_sys.asp");
eLK("e1d1L1","i","SDCA - Number Release","m_release_num_sdca_menu.asp");

eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};
eMF("e1d3","","<img src='images/h_utility_down.gif'  border='0'>","eCS('e1d2');eCS('e1d1');eCS('e1d4');eCS('e1d5');eCS('e1d6');");
eLK("e1d1L3","i","Number Search","r_num_search.asp");
eLK("e1d1L3","i","Free Number Availability","r_num_search_free.asp");
eLK("e1d1L3","i","Report - 2","r_num_search.htm");
eLK("e1d1L3","i","Report - 3","t_cust_search.htm");
eLK("e1d1L3","i","Report - 4","t_cust_search.htm");
eLK("e1d1L3","i","Report - 5","t_cust_search.htm");


eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};
eMF("e1d4","","<img src='images/h_resrve_down.gif'  border='0'>","eCS('e1d2');eCS('e1d3');eCS('e1d1');eCS('e1d5');eCS('e1d6');");
eLK("e1d1L4","i","Reservation Request","t_res_req_pro_Res.asp");
eLK("e1d1L4","i","Direct Allotment","t_res_req_pro_Multi.asp");
eLK("e1d1L4","i","Convert Reservation to Allotment","t_allot_req.asp");


eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};
eMF("e1d5","","<img src='images/h_numlife_down.gif'  border='0'>","eCS('e1d2');eCS('e1d3');eCS('e1d4');eCS('e1d1');eCS('e1d6');");
eLK("e1d1L5","i","Number Activation","t_number_active.asp");
eLK("e1d1L5","i","Number Termination","t_number_termi.asp");
eLK("e1d1L5","i","Reservation Release","t_reservation_request.asp");
eLK("e1d1L5","i","Quarantine Release","t_quarantine_release.asp");
eLK("e1d1L5","i","Cancellation","t_number_cancel.asp");
eLK("e1d1L5","i","Re-Allotment","t_re_allotment.asp");


eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};
eMF("e1d6","","<img src='images/h_vanity_down.gif'  border='0'>","eCS('e1d2');eCS('e1d3');eCS('e1d4');eCS('e1d5');eCS('e1d1');");
eLK("e1d1L6","i","Reservation Request","t_cust_search.htm");
eLK("e1d1L6","i","Direct Allotment","t_cust_search.htm");
eLK("e1d1L6","i","Convert Reservation to Allotment","t_cust_search.htm");
eLK("e1d1L6","i","Cancellation","t_res_req_pro_Multi_test.asp");


eDE();
//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");

eDW("<TR align='left'>");
eDW("<TD class='menuTreeBox' align='left' bgcolor='#DFE5CF' nowrap valign='top'>");
if(eNN&&ePI==4)
{
//eDW("<div>")
};
eMF("et1d1","","","");
eDE();

//if(eNN&&ePI==4){eDW("</div>")};
eDW("</TD>");
eDW("</TR>");
eDW("</TABLE>");
document.write(eGSTR);eGSTR="";

if(eMW3C){eF0()};
if(window.pageXOffset==0){eV0=true}else if(eIE){eV1=true};
if(eGB){eF1()}*/

