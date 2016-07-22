
(function () {
    olark.extend('CartSaver');
    olark.extend('Sounds');


    var isNewVersion = olark._ && olark._.versions && (olark._.versions.follow || olark._.versions.popout)
    if (isNewVersion) {
        olark._.finish({ "CartSaver": { "enabled": true, "version": "1.0.7", "type": "magento" }, "system": { "show_popout": 0, "allow_change_colors": true, "hashchange_events_trigger_page_change": 0, "operator_has_stopped_typing_text": "has stopped typing", "email_body_error_text": "You must complete all fields and specify a valid email address", "disable_default_visitor_information": 0, "hide_not_available": 0, "offline_message": "Live help is displayed for demo purposes only. To add it to your store please refer to the template documentation or \u003Ca href=\"http://www.olark.com/?r=ad8fbsj2\"\u003EOlark Live chat official website. \u003C/a\u003E", "allow_change_width": true, "show_pre_chat": 0, "require_offline_phone": 0, "allow_change_height": true, "say_text": "Type here and hit enter to chat", "habla_name_input_text": "click here and type your Name", "habla_offline_sent_text": "Thanks for your message!  We'll get back to you shortly.", "allow_mobile_boot": 0, "start_expanded": 0, "inline_css_url": "static.olark.com/css/c/b/cbb092d6554938a9549cb716ffcb1f94.css", "disable_extra_br": true, "bottom_margin": 0, "right_margin": 20, "allowed_domains": "", "habla_offline_email_text": "click here and type your Email", "close_hides_window": 0, "not_available_text": "Contact us!", "habla_offline_body_text": "We're not around but we still want to hear from you!  Leave us a note:", "top_margin": 0, "disable_width": true, "welcome_msg": "Questions? We'd love to chat!", "popout_css_url": "static.olark.com/css/9/8/98c23c22d4700f33524c3faf5aa12bd2.css", "disable_offline_messaging_fallback": true, "offline_msg_mode": 1, "habla_offline_phone_text": "click here and type your Phone", "inline_css_url_ie": "static.olark.com/css/7/f/7f64cf9c8017bad7f8bfbb157871daed.css", "inline_css_url_quirks": "static.olark.com/css/c/5/c57177fb5497e1053f613e9dbd8d4106.css", "start_hidden": 0, "ended_chat_message": "This chat has ended, start typing below if you need anything else!", "in_chat_text": "Now Chatting", "habla_offline_submit_value": "Send", "before_chat_text": "Chat with us!", "operator_is_typing_text": "is typing...", "left_margin": 20, "show_in_buddy_list": "all", "hkey": "PHNwYW4gc3R5bGU9ImRpc3BsYXk6bm9uZSI+PGEgaWQ9ImhibGluazkiPjwvYT5odHRwOi8vd3d3Lm9sYXJrLmNvbTwvc3Bhbj5HZXQgPGEgaHJlZj0iaHR0cDovL3d3dy5vbGFyay5jb20vP3JpZD03ODMwLTU4Mi0xMC0zNzE0JmFtcDtzYWxlcz0xJmFtcDt1dG1fbWVkaXVtPXdpZGdldCZhbXA7dXRtX2NhbXBhaWduPWZyZWVfc2FsZXMmYW1wO3V0bV9zb3VyY2U9NzgzMC01ODItMTAtMzcxNCIgaWQ9ImhibGluazk5IiAgdGFyZ2V0PSJfYmxhbmsiPkZyZWUgT2xhcmsgU2FsZXMgQ2hhdDwvYT4h", "md5": "b6f5ceb0020279285690608ffb200d30", "site_id": "7830-582-10-3714", "template": "azul", "operators": {} }, "Sounds": { "enabled": true }, "invalidate_cache": {} });
    } else {
        olark.configure(function (conf) {
            conf.system.site_id = "7830-582-10-3714";
        });
        olark._.finish();
    }
})();
