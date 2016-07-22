$(document).ready(function()
{if(tmolarkchat_status==2)
olark('api.box.hide');if(tmnewsletter_status==2)
{initTemplate();$(document).on('click','#newsletter_popup',function(event){event.stopPropagation();});$(document).on('click','.tmnewsletter-close, .newsletter-overlay',function(){closePopup();updateDate();});$(document).on('click','.tmnewsletter-submit',function(){submitNewsletter();});}
$('a').each(function()
{var href=$(this).attr('href');var search=this.search;var href_add='live_configurator&theme='+ get('theme')
+'&theme_font='+ get('theme_font');var baseDir_=baseDir.replace('https','http');if(location.href.indexOf('&theme')==-1&&location.href.indexOf('?theme')==-1&&location.href.indexOf('&theme_font')&&location.href.indexOf('?theme_font'))
return;if(typeof(href)!='undefined'&&href.substr(0,1)!='#'&&href.replace('https','http').substr(0,baseDir_.length)==baseDir_)
{if(search.length==0)
this.search=href_add;else
this.search+='&'+ href_add;}});$('#color-box').find('li').click(function()
{if(location.href.indexOf('live_configurator')==-1)
{if(location.href.indexOf('?')==-1)
location.href=location.href.replace(/&theme=[^&]*/,'')+'?live_configurator&theme='+$(this).attr('class');else
location.href=location.href.replace(/&theme=[^&]*/,'')+'&live_configurator&theme='+$(this).attr('class');}
else
location.href=location.href.replace(/&theme=[^&]*/,'')+'&theme='+$(this).attr('class');});$('#reset').click(function()
{location.href=location.href.replace(/&theme=[^&]*/,'').replace(/\?theme=[^&]*/,'').replace(/&theme_font=[^&]*/,'').replace(/\?theme_font=[^&]*/,'').replace(/&live_configurator[^&]*/,'').replace(/\?live_configurator[^&]*/,'');});$('#font').change(function()
{if(location.href.indexOf('live_configurator')==-1)
{if(location.href.indexOf('?')==-1)
location.href=location.href.replace(/&theme_font=[^&]*/,'')+'?live_configurator&theme_font='+$('#font option:selected').val();else
location.href=location.href.replace(/&theme_font=[^&]*/,'')+'&live_configurator&theme_font='+$('#font option:selected').val();}
else
location.href=location.href.replace(/&theme_font=[^&]*/,'')+'&theme_font='+$('#font option:selected').val();});$('#gear-right').click(function()
{if($(this).css('left')=='280px')
{$('#tool_customization').animate({left:'-280px'},500);$(this).animate({left:'0px'},500);$.totalStorage('live_configurator_visibility',0);}
else
{$('#tool_customization').animate({left:'0px'},500);$(this).animate({left:'280px'},500);$.totalStorage('live_configurator_visibility',1);}});$('#module-tmnewsletter').click(function(){if($(this).hasClass('disable'))
{$(this).removeClass('disable');}
else
{if(typeof(tmnewletter_user_message)!='undefined')
tmtcl_message=tmnewletter_user_message;else
tmtcl_message=html;setTemplate(tmtcl_message);$(this).addClass('disable');}
return false;});$('#module-tmolarkchat').click(function(){if($(this).hasClass('disable'))
{olark('api.box.hide');$(this).removeClass('disable');}
else
{olark('api.box.show');$(this).addClass('disable');}
return false;});if(parseInt($.totalStorage('live_configurator_visibility'))==1)
{$('#tool_customization').animate({left:'0px'},200);$('#gear-right').animate({left:'280px'},200);}
else
{$('#tool_customization').animate({left:'-280px'},200);$('#gear-right').animate({left:'0px'},200);}});function closePopup()
{$('#newsletter_popup, .newsletter-overlay').fadeOut(300,function(){$('#newsletter_popup').remove()});$('button#module-tmnewsletter').removeClass('disable');}
function get(name)
{var regexS="[\\?&]"+ name+"=([^&#]*)";var regex=new RegExp(regexS);var results=regex.exec(window.location.href);if(results==null)
return"";else
return results[1];}