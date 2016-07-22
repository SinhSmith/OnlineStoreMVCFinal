$(document).ready(function(){function openBranch(jQueryElement,noAnimation)
{jQueryElement.addClass('OPEN').removeClass('CLOSE');if(noAnimation)
jQueryElement.parent().find('ul:first').show();else
jQueryElement.parent().find('ul:first').slideDown();}
function closeBranch(jQueryElement,noAnimation)
{jQueryElement.addClass('CLOSE').removeClass('OPEN');if(noAnimation)
jQueryElement.parent().find('ul:first').hide();else
jQueryElement.parent().find('ul:first').slideUp();}
function toggleBranch(jQueryElement,noAnimation)
{if(jQueryElement.hasClass('OPEN'))
closeBranch(jQueryElement,noAnimation);else
openBranch(jQueryElement,noAnimation);}
$('.column ul.tree.dhtml ul').parent().find("span.grower").remove();$('.column ul.tree.dhtml ul').prev().before("<span class='grower OPEN'> </span>");$('.column ul.tree.dhtml ul li:last-child, .column ul.tree.dhtml li:last-child').addClass('last');$('.column ul.tree.dhtml span.grower.OPEN').addClass('CLOSE').removeClass('OPEN').parent().find('ul:first').hide();$('.column ul.tree.dhtml').show();$('.column ul.tree.dhtml .selected').parents().each(function(){if($(this).is('ul'))
toggleBranch($(this).prev().prev(),true);});toggleBranch($('.column ul.tree.dhtml .selected').prev(),true);var clickEventType=((document.ontouchstart!==null)?'click':'touchstart');$('.column ul.tree.dhtml span.grower').on(clickEventType,function(){toggleBranch($(this));});})