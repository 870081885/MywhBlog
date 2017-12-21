var welcome_notification = false; var custom_url = false; var chicklet_settings = false; welcome_notification = { "notification_title": "\u6b22\u8fce\u6765\u5230\u68a6\u5f71\u96fe\u82b1\u4e2a\u4eba\u535a\u5ba2", "notification_message": "\u8c22\u8c22\u8ba2\u9605\u68a6\u5f71\u96fe\u82b1\u4e2a\u4eba\u535a\u5ba2", "notification_url": "http:\/\/www.mywhblog.cn", "welcome_enabled": "true" }; chicklet_settings = { "enabled": true, "button_label": "\u83b7\u53d6\u63a8\u9001" }; var project_id = "360714148844"; var api_endpoint = "https://clients-api.pushengage.com/p/v1"; var swv = "2.0.0"; var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1; var is_chrome = !!window.chrome; var _peCookiesLoaded = false; var iframePermission = "waiting"; var is_firefox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1; var ff_str_pos = navigator.userAgent.toLowerCase().indexOf('firefox/') + 8; var cc_str_pos = navigator.userAgent.toLowerCase().indexOf('chrome/') + 7; var ff_version = parseInt(navigator.userAgent.substr(ff_str_pos, 2)); var cc_version = navigator.userAgent.substr(cc_str_pos, 2); var now = new Date(); var time = now.getTime(); var expireTime = time + (1000 * 60 * 60 * 24 * 7); now.setTime(expireTime); if (typeof (pe_http_box_loaded) == "undefined") var pe_http_box_loaded = false; function getCookie(name) {
    var nameEQ = name + "="; var ca = document.cookie.split(';'); for (var i = 0; i < ca.length; i++) { var c = ca[i]; while (c.charAt(0) == ' ') c = c.substring(1, c.length); if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length); }
    return null;
}
var _peinternal = []; if (typeof (_pedata) == "undefined")
    var _pedata = []; var first = false; navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) {
            if (!pushSubscription && Notification.permission != "denied") { first = true; }
            else { first = false; } 
        });
    }); _peinternal.sslsubscribe = function (callback) {
        if (_pe_optin_settings.desktop.optin_type != 4) { htmlbody = document.getElementsByTagName("BODY")[0]; var link = document.createElement('link'); link.rel = 'stylesheet'; link.type = 'text/css'; link.href = _peapp.app_subdomain + "/dialog.css"; link.media = 'screen'; htmlbody.appendChild(link); }
        var delay = _pe_optin_settings.desktop.optin_delay * 1000; (function waitforPushengageSubscriberID() {
            if (peGetCookie("PushSubscriberID") == "")
            { setTimeout(function () { waitforPushengageSubscriberID(); }, 100); }
            else
                if (first && ((is_chrome && cc_version >= 42) || (is_firefox && ff_version >= 44)) && peGetCookie("peclosed") === "" && peGetCookie("PushSubscriberID") === "false" && pe_http_box_loaded == false) {
                    pe_http_box_loaded = true; setTimeout(function () {
                        htmlbody = document.getElementsByTagName("BODY")[0]; if (dialog_box) _pe_optin_settings.desktop.optin_type = 4; if (_pe_optin_settings.desktop.optin_type == 1 || typeof _pe_optin_settings.desktop.optin_type == "undefined")
                            htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' style='position:fixed;width:435px;top:0px;left:33%;border: 1px solid #D0D0D0;background: #EFEFEF;padding:15px;-webkit-border-radius: 3px;-moz-border-radius: 3px;border-radius: 3px;box-shadow: 1px 1px 3px #DCDCDC;z-index: 999999;'><div style='float: left;padding: -1px;margin-right: 8px;width:80px;height:80px;' id='pushengage_client_img'><img src='" + _peapp.app_image + "' style='width: 87px;'></div>  <div style='font-family: arial;font-size: 15px;font-weight: 600;color: #4A4A4A;' id='pushengage_dialog_content'>" + _pe_optin_settings.desktop.optin_title + "</div>  <div style='clear: both;'><div style='float: left;font-family: arial;font-size: 9px;padding-top: 10px;'>" + _peapp.app_poweredby + "</div><div style='float: right;font-family: arial;padding: 1px 19px;font-size: 15px;background-color: #2ecc71;color: #fff;border: 1px solid #7FB797;border-radius: 4px;cursor:pointer;' id='pushengage_allow_btn' >" + _pe_optin_settings.desktop.optin_allow_btn_txt + "</div><div style='float: right;font-family: arial;font-size: 15px;padding: 1px 19px;background-color: #fff;border-radius: 5px;border: 1px solid #D6D1D1;margin-right: 7px;cursor:pointer;' id='pushengage_close_btn'>" + _pe_optin_settings.desktop.optin_close_btn_txt + "</div>  </div>  </div> "); if (_pe_optin_settings.desktop.optin_type == 2)
                            htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='optin-3 optin-floatin' style='transition-duration: 1.5s;'><div class='cls-btn' id='pushengage_close_btn'><i class='fa fa-close'></i></div><div class='pe_logo'><img src='" + _peapp.app_image + "'></div><div class='pe_title'>" + _pe_optin_settings.desktop.optin_title + "</div><div class='pe_buttons'><input type='button' value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "' id='pushengage_allow_btn' class='pe_btn-allow allow-btn'></div><div class='pe_branding'><a href='http://www.pushengage.com/' target='_blank'>" + _peapp.app_poweredby + "</a></div></div>"); if (_pe_optin_settings.desktop.optin_type == 3)
                            htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='PE-optin4'><div class='PE-optin4-box PE-arrow_box '><div class='PE-optin4-image' style='padding-top:10px'><img src='" + _peapp.app_image + "' style='border-radius:50%'></div><div class='PE-optin4-text'><span id='PEnoti-close-pane' onclick='PEleft_hide_sidebar(); PESwingWellSetOption4();'><i class='fa fa-close'></i></span><i id='pushengage_close_btn'></i><div class='PE-title PE-optin4-heading' style='padding-top:10px'>" + _pe_optin_settings.desktop.optin_title + "</div></div><div class='PE-optin4-btns'><input type='button' class='PE-push-btn PE-btn-allow'  value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "'></div><div class='PE-branding'><a href='https://www.pushengage.com' target='_blank'>" + _peapp.app_poweredby + "</a></div></div><div class='PE-optin4-bell' id='pushengage_allow_btn' ><i class='fa fa-bell PEoption4bell PEnotioption4-swing'></i></div></div>"); if (_pe_optin_settings.desktop.optin_type == 4 || _pe_optin_settings.desktop.optin_type == 5) {
                            function checkForpe() { setTimeout(function () { if (typeof pe != 'undefined') return pe.subscribe(callback); return checkForpe(); }, 50); }
                            return checkForpe();
                        }
                        var head = document.getElementsByTagName('head')[0]; var link = document.createElement('link'); link.rel = 'stylesheet'; link.type = 'text/css'; link.href = _peapp.app_subdomain + "/dialog.css"; link.media = 'screen'; htmlbody.appendChild(link); host = location.host; domainParts = host.split('.'); if (domainParts.length > 2) { var reducelen = domainParts.length - 2; }
                        for (var i = 0; i < reducelen; i++) { domainParts.shift(); }; domain = '.' + domainParts.join('.'); pe_allow_btn = document.getElementById("pushengage_allow_btn"); pe_allow_btn.addEventListener("click", function () {
                            pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; if (custom_url) {
                                link = custom_url.url; document.cookie = "peclosed=true; expires=" + now.toGMTString() + ";path=/;"; if (custom_url.type == "window") { var _pewin = window.open(link, "_blank", "width=192, height=185"); }
                                else if (custom_url.type == "tab") { var _pewin = window.open(link); } 
                            }
                            else
                                pe.subscribe(callback);
                        }); pe_close_btn = document.getElementById("pushengage_close_btn"); pe_close_btn.addEventListener("click", function () { pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; document.cookie = "peclosed=true; expires=" + now.toGMTString() + ";path=/;"; });
                    }, delay);
                } 
        } ());
    }
function loadCookieIframe() {
    htmlbody1 = document.getElementsByTagName("BODY")[0]; if (getCookie("PushSubscriberID") == "" || getCookie("PushSubscriberID") == null || getCookie("PushSubscriberID") == "false")
        htmlbody1.insertAdjacentHTML('beforeend', '<iframe src="https://mywh.pushengage.com/cookie.php" style="display: none;"></iframe>'); else
        _peCookiesLoaded = true; _pedata.forEach(function (item, index) { if (item.action == "subscribe") { _pe.subscribe(); } });
}
if (document.readyState == "complete")
    loadCookieIframe(); window.addEventListener("load", loadCookieIframe, false); window.addEventListener('message', function (event) {
        if (event.origin !== 'https://mywh.pushengage.com') return; subscriber_data = JSON.parse(event.data); if (typeof (subscriber_data.state) != "undefined") {
            if (subscriber_data.state == "default")
                document.cookie = "peclosed=true; expires=" + now.toGMTString() + ";path=/;"; if (subscriber_data.state == "granted")
                peShowContent(); iframePermission = subscriber_data.state;
        }
        else { _peCookiesLoaded = true; document.cookie = 'isPushEnabled=' + subscriber_data.isPushEnabled.toString() + '; expires=Fri, 3 Aug 2222 20:47:11 UTC; path=/'; document.cookie = 'PushSubscriberID=' + subscriber_data.PushSubscriberID.toString() + '; expires=Fri, 3 Aug 2222 20:47:11 UTC; path=/'; }
        _pedata.forEach(function (item, index) {
            if (item.action == "addSubscriberToSegment") { _pe.addSubscriberToSegment(item.data) }
            if (item.action == "removeSubscriberFromSegment") { _pe.removeSubscriberFromSegment(item.data) } 
        });
    }, false); function PEleft_hide_sidebar()
    { document.querySelector('.PE-optin4-box').style.display = "none"; }
function PEleft_show_sidebar()
{ document.querySelector('.PE-optin4-box').style.display = "block"; }
var PEswingwell = ""; function PESwingWellSetOption4()
{ PEswingwell = setInterval(function () { startWellSwing() }, 1000); }
function startWellSwing() {
    var elements = document.getElementsByClassName('fa fa-bell PEoption4bell'); for (var i = 0; i < elements.length; i++) {
        var element = elements[i]; if (element.className == 'fa fa-bell PEoption4bell')
            element.className += ' PEnotioption4-swing'; else
            element.className = 'fa fa-bell PEoption4bell';
    } 
}
PESwingWellSetOption4(); function stopWellSwing() { clearInterval(PEswingwell); }
function peShowContent() { document.querySelector(".pushengagesweet-alert").style.display = "block"; document.querySelector(".pushengagesweet-overlay").style.display = "block"; document.querySelector(".pushengagesweet-overlay").style.opacity = 0.8; document.querySelector(".pushengagesweet-alert").className = "pushengagesweet-alert pushengage-visible showpushengagesweetAlert"; }
function hideAlert(segment) {
    document.querySelector(".pushengagesweet-overlay").style.display = "none"; document.querySelector(".pushengagesweet-overlay").style.opacity = 0; document.querySelector(".pushengage-visible").className = "pushengagesweet-alert hidepushengagesweetAlert"; document.querySelector(".hidepushengagesweetAlert").style.display = "none"; var link = _peapp.app_subdomain + "?action=subscribe"; if (segment != 'undefined' && (typeof segment !== "undefined")) { link = link + "&segment=" + segment; }
    if (custom_url) {
        link = custom_url.url; if (custom_url.type == "window") { var _pewin = window.open(link, "_blank", "width=192, height=185"); }
        else if (custom_url.type == "tab") { var _pewin = window.open(link); } 
    }
    else
        window.open(link, "_blank", "width=192, height=185");
}
function attachIframe() {
    (function waitforPushengageSubscriberID() {
        if (peGetCookie("PushSubscriberID") == "")
            setTimeout(function () { waitforPushengageSubscriberID(); }, 100); else { if (peGetCookie("peclosed") === "" && (peGetCookie("PushSubscriberID") === "false")) { iframe = document.createElement("IFRAME"), iframe.setAttribute("src", _peapp.app_subdomain + "/iframe.html"), iframe.style.width = "0px", iframe.style.height = "0px", iframe.style.border = "0px", iframe.setAttribute("visibility", "hidden"), iframe.style.display = "none"; if (document.body) document.body.appendChild(iframe); else document.head.appendChild(iframe); } } 
    } ());
}
function addAlertHtml(segment) { htmlbody = document.getElementsByTagName("BODY")[0]; htmlbody.insertAdjacentHTML('beforeend', '<div class="pushengagesweet-overlay" tabindex="-1" style="opacity: 1.1; display: none;"></div><div class="pushengagesweet-alert showpushengagesweetAlert pushengage-visible" data-custom-class="" data-has-cancel-button="false" data-has-confirm-button="true" data-allow-outside-click="false" data-has-done-function="false" data-animation="pop" data-timer="null" style="display: none;margin-top: -122px;"><div class="pushengagesweet-alert-content"><h2>' + _pe_optin_settings.desktop.optin_title + '</h2><p class="pushengagesweet-alert-poweredby">' + _peapp.app_poweredby + '</p></div><div class="sa-button-container"><div class="sa-confirm-button-container"  onclick="hideAlert(\'' + segment + '\');"><button class="confirm" tabindex="1">CLOSE</button></div></div></div>'); }
function attachDialogCss()
{ var head = document.getElementsByTagName('head')[0]; var link = document.createElement('link'); link.rel = 'stylesheet'; link.type = 'text/css'; link.href = _peapp.app_subdomain + "/dialog.css"; link.media = 'screen'; htmlbody.appendChild(link); }
if (typeof (pathvars) == "undefined")
    var pathvars = { worker: "/service-worker.js", manifest: "/manifest.json" }; var internalsegment = false; var _peapp = { "app_key": "b8b6b2456502b3166c0eca06bd41656c", "app_id": "9149", "app_name": "mywhblog", "app_subdomain": "https://mywh.pushengage.com", "app_image": "https://assetscdn.pushengage.com/site_images/9149d76a61489718087.jpg", "app_poweredby": "powered by PushEngage", "app_url": "http://www.pushengage.com" }; var _pe_optin_settings = { "desktop": { "http": { "optin_delay": 2, "optin_type": 1, "optin_title": "\u4f60\u5141\u8bb8\u4ece\u63a5\u53d7\u4ece\u68a6\u5f71\u96fe\u82b1\u4e2a\u4eba\u535a\u5ba2\u63a8\u9001\u7684\u6587\u7ae0\u5417\uff1f", "optin_allow_btn_txt": "\u5141\u8bb8", "optin_close_btn_txt": "\u4e0d\u5141\u8bb8", "optin_font": null }, "https": { "optin_delay": 1, "optin_type": 4, "optin_title": "Thank you for subscribing to our Push Notifications", "optin_allow_btn_txt": "Allow", "optin_close_btn_txt": "Close"} }, "mobile": { "optin_delay": 1, "optin_type": 4, "optin_title": "Thank you for subscribing to our Push Notifications", "optin_allow_btn_txt": "Allow", "optin_close_btn_txt": "Close" }, "intermediate": { "page_heading": "\u70b9\u51fb\u6b64\u5904\u5c06\u5141\u8bb8\u83b7\u53d6\u6765\u81ea\u68a6\u5f71\u96fe\u82b1\u535a\u5ba2\u7684\u901a\u77e5", "page_tagline": "\u8bf7\u70b9\u51fb\u4e0a\u65b9\u6587\u5b57\u5141\u8bb8\u83b7\u53d6\u901a\u77e5"} }; var _pehost = "https://pushengage.com/"; function peGetCookie(cname) {
        if (localStorage.getItem(cname) != null) return localStorage.getItem(cname); var name = cname + "="; var ca = document.cookie.split(';'); for (var i = 0; i < ca.length; i++) { var c = ca[i]; while (c.charAt(0) == ' ') c = c.substring(1); if (c.indexOf(name) == 0) return c.substring(name.length, c.length); }
        return "";
    }
if (location.protocol === "http:") {
    if (typeof (_pe_optin_settings.desktop.http) == "object")
        _pe_optin_settings.desktop = _pe_optin_settings.desktop.http; var _pe = { openDialogBox: function (segment) {
            var link = _peapp.app_subdomain + "?action=subscribe"; if (typeof segment !== 'undefined') { link = link + "&segment=" + segment; }
            var _pewin = window.open(link, "_blank", "width=800, height=600");
        }, addSubscriberToSegment: function (segmentName) { if (getCookie("PushSubscriberID") == null || getCookie("PushSubscriberID") == "" || getCookie("PushSubscriberID") == "false" || typeof (segmentName) == "undefined" || segmentName == "false" || segmentName == "" || segmentName == false) return true; var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/segments/add?swv=" + swv + "&bv=" + get_browser(), true); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": getCookie("PushSubscriberID"), "segment": segmentName, "site_id": "9149" }); xhttp.send(data); _pedata.push({ "action": "addSubscriberToSegment", "data": segmentName }); return true; }, removeSubscriberFromSegment: function (segmentName) { if (getCookie("PushSubscriberID") == null || getCookie("PushSubscriberID") == "") return true; var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/segments/remove?swv=" + swv + "&bv=" + get_browser(), true); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": getCookie("PushSubscriberID"), "segment": segmentName }); xhttp.send(data); _pedata.push({ "action": "removeSubscriberFromSegment", "data": segmentName, "site_id": "9149" }); return true; }, addProfileId: function (profileId) { var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/profile-id/add?swv=" + swv + "&bv=" + get_browser(), false); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": getCookie("PushSubscriberID"), "profile_id": profileId, "site_id": "9149" }); xhttp.send(data); _pedata.push({ "action": "addProfileId", "data": profileId }); return true; }, iframe_subscribe: function (segment) {
            if (document.readyState == "complete") { attachIframe(); addAlertHtml(segment); attachDialogCss(); }
            else
                window.addEventListener("load", function () { attachIframe(); addAlertHtml(segment); attachDialogCss(); });
        }, subscribe: function (segment) {
            if (_pe_optin_settings.desktop.optin_type == 4)
            { var delay = _pe_optin_settings.desktop.optin_delay * 1000; setTimeout(function function_name(argument) { _pe.iframe_subscribe(segment); }, delay); return true; }
            (function waitforPushengageSubscriberID() {
                if (peGetCookie("PushSubscriberID") == "")
                { setTimeout(function () { waitforPushengageSubscriberID(); }, 100); }
                else
                    if (peGetCookie("peclosed") === "" && (peGetCookie("PushSubscriberID") === "" || peGetCookie("PushSubscriberID") === "false") && pe_http_box_loaded == false) {
                        pe_http_box_loaded = true; if (document.readyState == "complete") {
                            var delay = _pe_optin_settings.desktop.optin_delay * 1000; if ((is_chrome && cc_version >= 42) || (is_firefox && ff_version >= 44))
                                setTimeout(function () {
                                    htmlbody = document.getElementsByTagName("BODY")[0]; if (_pe_optin_settings.desktop.optin_type == 1 || typeof _pe_optin_settings.desktop.optin_type == "undefined")
                                    { htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' style='position:fixed;width:435px;top:0px;left:33%;border: 1px solid #D0D0D0;background: #EFEFEF;padding:15px;-webkit-border-radius: 3px;-moz-border-radius: 3px;border-radius: 3px;box-shadow: 1px 1px 3px #DCDCDC;z-index: 999999;'><div style='float: left;padding: -1px;margin-right: 8px;width:80px;height:80px;' id='pushengage_client_img'><img src='" + _peapp.app_image + "' style='width: 87px;'></div>  <div style='font-family: arial;font-size: 15px;font-weight: 600;color: #4A4A4A;' id='pushengage_dialog_content'>" + _pe_optin_settings.desktop.optin_title + "</div>  <div style='clear: both;'><div style='float: left;font-family: arial;font-size: 9px;padding-top: 10px;'>" + _peapp.app_poweredby + "</div><div style='float: right;font-family: arial;padding: 1px 19px;font-size: 15px;background-color: #2ecc71;color: #fff;border: 1px solid #7FB797;border-radius: 4px;cursor:pointer;' id='pushengage_allow_btn' >" + _pe_optin_settings.desktop.optin_allow_btn_txt + "</div><div style='float: right;font-family: arial;font-size: 15px;padding: 1px 19px;background-color: #fff;border-radius: 5px;border: 1px solid #D6D1D1;margin-right: 7px;cursor:pointer;' id='pushengage_close_btn'>" + _pe_optin_settings.desktop.optin_close_btn_txt + "</div>  </div>  </div> "); }
                                    if (_pe_optin_settings.desktop.optin_type == 2)
                                        htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='optin-3 optin-floatin' style='transition-duration: 1.5s;'><div class='cls-btn' id='pushengage_close_btn'><i class='fa fa-close'></i></div><div class='pe_logo'><img src='" + _peapp.app_image + "'></div><div class='pe_title'>" + _pe_optin_settings.desktop.optin_title + "</div><div class='pe_buttons'><input type='button' value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "' id='pushengage_allow_btn' class='pe_btn-allow allow-btn'></div><div class='pe_branding'><a href='http://www.pushengage.com/' target='_blank'>" + _peapp.app_poweredby + "</a></div></div>"); if (_pe_optin_settings.desktop.optin_type == 3)
                                        htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='PE-optin4'><div class='PE-optin4-box PE-arrow_box '><div class='PE-optin4-image' style='padding-top:10px'><img src='" + _peapp.app_image + "' style='border-radius:50%'></div><div class='PE-optin4-text'><span id='PEnoti-close-pane' onclick='PEleft_hide_sidebar(); PESwingWellSetOption4();'><i class='fa fa-close'></i></span><i id='pushengage_close_btn'></i><div class='PE-title PE-optin4-heading' style='padding-top:10px'>" + _pe_optin_settings.desktop.optin_title + "</div></div><div class='PE-optin4-btns'><input type='button' class='PE-push-btn PE-btn-allow'  value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "'></div><div class='PE-branding'><a href='https://www.pushengage.com' target='_blank'>" + _peapp.app_poweredby + "</a></div></div><div class='PE-optin4-bell' id='pushengage_allow_btn' ><i class='fa fa-bell PEoption4bell PEnotioption4-swing'></i></div></div>"); var head = document.getElementsByTagName('head')[0]; var link = document.createElement('link'); link.rel = 'stylesheet'; link.type = 'text/css'; link.href = _peapp.app_subdomain + "/dialog.css"; link.media = 'screen'; htmlbody.appendChild(link); host = location.host; domainParts = host.split('.'); if (domainParts.length > 2) { var reducelen = domainParts.length - 2; }
                                    for (var i = 0; i < reducelen; i++) { domainParts.shift(); }; domain = '.' + domainParts.join('.'); pe_allow_btn = document.getElementById("pushengage_allow_btn"); pe_allow_btn.addEventListener("click", function () {
                                        var link = _peapp.app_subdomain + "?action=subscribe"; if (typeof segment !== 'undefined') { link = link + "&segment=" + segment; }
                                        if (custom_url) {
                                            link = custom_url.url; if (custom_url.type == "window") { var _pewin = window.open(link, "_blank", "width=800, height=600"); }
                                            else if (custom_url.type == "tab") { var _pewin = window.open(link); } 
                                        }
                                        else
                                            var _pewin = window.open(link, "_blank", "width=800, height=600"); pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; document.cookie = "peclosed=true; expires=Fri, 3 Aug 2222 20:47:11 UTC;path=/;"; localStorage.setItem('peclosed', true);
                                    }); pe_close_btn = document.getElementById("pushengage_close_btn"); pe_close_btn.addEventListener("click", function () { pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; document.cookie = "peclosed=true; expires=" + now.toGMTString() + ";path=/;"; });
                                }, delay);
                        }
                        window.addEventListener("load", function () {
                            _pedata.forEach(function (item, index) { if (item.action == "addSubscriberToSegment") segment = item.data; }); var delay = _pe_optin_settings.desktop.optin_delay * 1000; if ((is_chrome && cc_version >= 42) || (is_firefox && ff_version >= 44))
                                setTimeout(function () {
                                    htmlbody = document.getElementsByTagName("BODY")[0]; if (_pe_optin_settings.desktop.optin_type == 1 || typeof _pe_optin_settings.desktop.optin_type == "undefined") { htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' style='position:fixed;width:435px;top:0px;left:33%;border: 1px solid #D0D0D0;background: #EFEFEF;padding:15px;-webkit-border-radius: 3px;-moz-border-radius: 3px;border-radius: 3px;box-shadow: 1px 1px 3px #DCDCDC;z-index: 999999;'><div style='float: left;padding: -1px;margin-right: 8px;width:80px;height:80px;' id='pushengage_client_img'><img src='" + _peapp.app_image + "' style='width: 87px;'></div>  <div style='font-family: arial;font-size: 15px;font-weight: 600;color: #4A4A4A;' id='pushengage_dialog_content'>" + _pe_optin_settings.desktop.optin_title + "</div>  <div style='clear: both;'><div style='float: left;font-family: arial;font-size: 9px;padding-top: 10px;'>" + _peapp.app_poweredby + "</div><div style='float: right;font-family: arial;padding: 1px 19px;font-size: 15px;background-color: #2ecc71;color: #fff;border: 1px solid #7FB797;border-radius: 4px;cursor:pointer;' id='pushengage_allow_btn' >" + _pe_optin_settings.desktop.optin_allow_btn_txt + "</div><div style='float: right;font-family: arial;font-size: 15px;padding: 1px 19px;background-color: #fff;border-radius: 5px;border: 1px solid #D6D1D1;margin-right: 7px;cursor:pointer;' id='pushengage_close_btn'>" + _pe_optin_settings.desktop.optin_close_btn_txt + "</div>  </div>  </div> "); }
                                    if (_pe_optin_settings.desktop.optin_type == 2)
                                        htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='optin-3 optin-floatin' style='transition-duration: 1.5s;'><div class='cls-btn' id='pushengage_close_btn'><i class='fa fa-close'></i></div><div class='pe_logo'><img src='" + _peapp.app_image + "'></div><div class='pe_title'>" + _pe_optin_settings.desktop.optin_title + "</div><div class='pe_buttons'><input type='button' value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "' id='pushengage_allow_btn' class='pe_btn-allow allow-btn'></div><div class='pe_branding'><a href='http://www.pushengage.com/' target='_blank'>" + _peapp.app_poweredby + "</a></div></div>"); if (_pe_optin_settings.desktop.optin_type == 3)
                                        htmlbody.insertAdjacentHTML('beforeend', "<div id='pushengage_confirm' class='PE-optin4'><div class='PE-optin4-box PE-arrow_box '><div class='PE-optin4-image' style='padding-top:10px'><img src='" + _peapp.app_image + "' style='border-radius:50%'></div><div class='PE-optin4-text'><span id='PEnoti-close-pane' onclick='PEleft_hide_sidebar(); PESwingWellSetOption4();'><i class='fa fa-close'></i></span><i id='pushengage_close_btn'></i><div class='PE-title PE-optin4-heading' style='padding-top:10px'>" + _pe_optin_settings.desktop.optin_title + "</div></div><div class='PE-optin4-btns'><input type='button' class='PE-push-btn PE-btn-allow'  value='" + _pe_optin_settings.desktop.optin_allow_btn_txt + "'></div><div class='PE-branding'><a href='https://www.pushengage.com' target='_blank'>" + _peapp.app_poweredby + "</a></div></div><div class='PE-optin4-bell' id='pushengage_allow_btn' ><i class='fa fa-bell PEoption4bell PEnotioption4-swing'></i></div></div>"); var head = document.getElementsByTagName('head')[0]; var link = document.createElement('link'); link.rel = 'stylesheet'; link.type = 'text/css'; link.href = _peapp.app_subdomain + "/dialog.css"; link.media = 'screen'; htmlbody.appendChild(link); host = location.host; domainParts = host.split('.'); if (domainParts.length > 2) { var reducelen = domainParts.length - 2; }
                                    for (var i = 0; i < reducelen; i++) { domainParts.shift(); }; domain = '.' + domainParts.join('.'); pe_allow_btn = document.getElementById("pushengage_allow_btn"); pe_allow_btn.addEventListener("click", function () {
                                        var link = _peapp.app_subdomain + "?action=subscribe"; if (typeof segment !== 'undefined') { link = link + "&segment=" + segment; }
                                        if (custom_url) {
                                            link = custom_url.url; if (typeof segment !== 'undefined') { link = link + "?segment=" + segment; }
                                            if (custom_url.type == "window") { var _pewin = window.open(link, "_blank", "width=800, height=600"); }
                                            else if (custom_url.type == "tab") { var _pewin = window.open(link); } 
                                        }
                                        else
                                            var _pewin = window.open(link, "_blank", "width=800, height=600"); pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; document.cookie = "peclosed=true; expires=Fri, 3 Aug 2222 20:47:11 UTC;path=/;"; localStorage.setItem('peclosed', true);
                                    }); pe_close_btn = document.getElementById("pushengage_close_btn"); pe_close_btn.addEventListener("click", function () { pushengage_confirm = document.getElementById("pushengage_confirm"); pushengage_confirm.style.display = "none"; document.cookie = "peclosed=true; expires=" + now.toGMTString() + ";path=/;"; });
                                }, delay);
                        }, false);
                    } 
            } ());
        } 
        };
}
if (location.protocol === "https:") {
    if (typeof (_pe_optin_settings.desktop.https) == "object")
        _pe_optin_settings.desktop = _pe_optin_settings.desktop.https; var script = document.createElement('script'); script.type = "text/javascript"; script.src = _peapp.app_subdomain + "/script.js"; document.getElementsByTagName('head')[0].appendChild(script); var _pe = { subscribe: function (segmentName, callback) {
            _pedata.push({ "action": "addSubscriberToSegment", "data": segmentName }); var delay = _pe_optin_settings.desktop.optin_delay * 1000; if (typeof segmentName != 'undefined') {
                if (!internalsegment)
                    internalsegment = segmentName;
            }
            if (internalsegment != false) { segment = internalsegment; }
            if (document.readyState == "complete") { return _peinternal.sslsubscribe(callback); }
            window.addEventListener("load", function () { _peinternal.sslsubscribe(callback); }, false);
        }, addSubscriberToSegment: function (segmentName) {
            if (typeof (segmentName) == "undefined" || segmentName == "false" || segmentName == "" || segmentName == false) { return false; }
            var isPushEnabled = false; var PushSubscriberID = false; navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
                serviceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) {
                    if (!pushSubscription) { isPushEnabled = false; return; }
                    else { isPushEnabled = true; PushSubscriberID = pushSubscription.endpoint.split("/")[pushSubscription.endpoint.split("/").length - 1]; var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/segments/add?swv=" + swv + "&bv=" + get_browser(), false); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": PushSubscriberID, "segment": segmentName, "site_id": "9149" }); xhttp.send(data); } 
                });
            }); return true;
        }, addProfileId: function (profileId) {
            _pedata.push({ "action": "addProfileId", "data": profileId }); var isPushEnabled = false; var PushSubscriberID = false; navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
                serviceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) {
                    if (!pushSubscription) { isPushEnabled = false; return; }
                    else { isPushEnabled = true; PushSubscriberID = pushSubscription.endpoint.split("/")[pushSubscription.endpoint.split("/").length - 1]; var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/profile-id/add?swv=" + swv + "&bv=" + get_browser(), false); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": PushSubscriberID, "profile_id": profileId, "site_id": "9149" }); xhttp.send(data); } 
                });
            }); return true;
        }, removeSubscriberFromSegment: function (segmentName) {
            var isPushEnabled = false; var PushSubscriberID = false; navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
                serviceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) {
                    if (!pushSubscription) { isPushEnabled = false; return; }
                    else { isPushEnabled = true; PushSubscriberID = pushSubscription.endpoint.split("/")[pushSubscription.endpoint.split("/").length - 1]; var xhttp = new XMLHttpRequest(); xhttp.open("POST", api_endpoint + "/subscriber/segments/remove?swv=" + swv + "&bv=" + get_browser(), true); xhttp.setRequestHeader("Content-type", "application/json"); var data = JSON.stringify({ "device_token": PushSubscriberID, "segment": segmentName, "site_id": "9149" }); xhttp.send(data); } 
                });
            }); return true;
        }, isSubscribed: function (callback) { navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) { serviceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) { callback(pushSubscription != null && typeof (pushSubscription) == "object"); }); }); } 
        };
}
function get_browser() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || []; if (/trident/i.test(M[1])) { tem = /\brv[ :]+(\d+)/g.exec(ua) || []; return { name: 'IE', version: (tem[1] || '') }; }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR|Edge\/(\d+)/)
        if (tem != null) { return { name: 'Opera', version: tem[1] }; } 
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?']; if ((tem = ua.match(/version\/(\d+)/i)) != null) { M.splice(1, 1, tem[1]); }
    return M[1];
}
(function _peinitialise() {
    var is_mobile = screen.width <= 800; var is_chicklet_enabled = chicklet_settings && chicklet_settings.enabled; if (is_mobile || !is_chicklet_enabled) return false; if (typeof (chicklet_settings.button_label) == "undefined") chicklet_settings.button_label = "Get Notifications"; var condition1 = !_peCookiesLoaded; var condition2 = _pe_optin_settings.desktop.optin_type == 4 && window.location.protocol == "http:" && iframePermission == "waiting"; var condition3 = document.readyState != "complete"; if (condition1 || condition2 || condition3)
    { setTimeout(function () { _peinitialise(); }, 100); }
    else {
        var docbody = document.getElementsByTagName("BODY")[0]; var htmldeny = peGetCookie("peclosed") == "true" && peGetCookie("isPushEnabled") == "false"; var nativedeny = Notification.permission == "denied"; var iframedeny = iframePermission == "denied"; var isSsl = window.location.protocol == "https:"; if (htmldeny || nativedeny || iframedeny) {
            docbody.insertAdjacentHTML('beforeend', '<style>#PE-chicklet{position:fixed!important;top:100px!important;right:0!important;border-radius:4px 4px 0 0!important;font-size:18px!important;color:#fff!important;background-color:#042048!important;transform:rotate(-90deg) translateY(35px);transform-origin:bottom right!important;transition-duration:1s!important;cursor:pointer!important;z-index:99999!important;font-weight:400!important;line-height:34px!important}#PE-chicklet .PE-chicklet-content{padding:6px 25px 6px 0!important}</style>'); docbody.insertAdjacentHTML('beforeend', '<div id="PE-chicklet"><a href="https://www.pushengage.com"><img src="https://www.pushengage.com/favicon.ico" alt="pushengage logo" style="padding: 0px 10px 0px 15px;margin: 0px -7px -2px 0px;" /></a> <span class="PE-chicklet-content">' + chicklet_settings.button_label + '</span></div>'); docbody.insertAdjacentHTML('beforeend', "<style>#PE-chicklet-inst-modal{display:none;position:fixed;z-index:999999;left:0;top:0;width:100%;height:100%;overflow:auto;background-color:#000;background-color:rgba(0,0,0,.4);overflow-x:hidden}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content{background-color:#fefefe;left:50%;transform:translate(-50%,-50%);top:50%;position:absolute;border-radius:3px}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body #PE-step-img{display:block;margin:0 auto;box-shadow:0 0 5px #bbb}@media(max-width:560px){#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content{width:100%;left:0;top:0;transform:translate(0,0)}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body #PE-step-img{argin:auto;width:100%}}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body{padding:20px}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body #PE-button-box{float:right;padding:40px}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body #PE-button-box a#PE-cancel-btn{color:#042048;text-decoration:none}#PE-chicklet-inst-modal #PE-chicklet-inst-modal-content #PE-chicklet-inst-modal-body #PE-button-box a#PE-retry-btn{box-shadow:0 7px 14px rgba(50,50,93,.1),0 3px 6px rgba(0,0,0,.08);margin:0 18px;height:45px;line-height:45px;border-radius:2px;background:#449afe;color:#fff;padding:10px 16px;text-decoration:none}</style>"); docbody.insertAdjacentHTML('beforeend', "<div id=PE-chicklet-inst-modal><div id=PE-chicklet-inst-modal-content><div id=PE-chicklet-inst-modal-body><p style=font-size:18px;margin-bottom:40px;line-height:24px;text-align:center>You seem to have previously blocked/denied push notifications. See the image below to know how to unblock them.</p><img id=PE-step-img src=https://assetscdn.pushengage.com/site_assets/img/dbgq43.jpg><div id=PE-button-box><a href=# id=PE-cancel-btn>Cancel</a> <a href=# id=PE-retry-btn>Retry</a></div></div></div></div>"); var PEChickletInstModalClose = document.getElementById("PE-cancel-btn"); var PEChickletInstModalRetry = document.getElementById("PE-retry-btn"); PEChickletInstModalClose.onclick = function () { PEChickletInstModal.style.display = "none"; }
            PEChickletInstModalRetry.onclick = function () {
                if (Notification.permission != "denied")
                { first = true; pe.subscribe(); PEChickletInstModal.style.display = "none"; } 
            }
            var PEChickletInstModal = document.getElementById("PE-chicklet-inst-modal"); document.getElementById("PE-chicklet").style.transform = "rotate(-90deg)"; pe_chicklet = document.getElementById("PE-chicklet"); pe_chicklet.addEventListener("click", function () {
                if (isSsl) {
                    if (nativedeny)
                        PEChickletInstModal.style.display = "block"; else if (htmldeny)
                        pe.subscribe();
                }
                else {
                    if (htmldeny || iframedeny)
                    { var link = _peapp.app_subdomain + "?action=subscribe&permission=denied"; var window_size = { "width": 450, "height": 600 }; window.open(link, "_blank", "width=" + window_size.width + ", height=" + window_size.height + ""); } 
                }
                pushengage_confirm = document.getElementById("PE-chicklet"); pushengage_confirm.style.display = "none";
            });
        } 
    } 
})();