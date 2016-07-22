var instantSearchQueries=[];$(document).ready(function()
{var $input=$("#tm_search_query");var width_ac_results=$input.parent('form').width();if(typeof ajaxsearch!='undefined'&&ajaxsearch){if(typeof isMobile!='undefined'&&!isMobile){$input.autocomplete(search_url_local,{minChars:3,max:10,width:(width_ac_results>0?width_ac_results:500),selectFirst:false,scroll:tmsearch_scroll,scrollHeight:tmsearch_height,dataType:"json",formatItem:function(data,i,max,value,term){return value;},parse:function(data){var mytab=[];tmsearch_limit&&tmsearch_limit_num<data.length?showeditems=tmsearch_limit_num:showeditems=data.length;for(var i=0;i<showeditems;i++)
{html='';if(tmsearch_image)
html+='<div class="pull-left"><img class="img-responsive" src="'+data[i].img_url+'" alt="'+data[i].name+'" /></div>';html+='<div class="content">';html+='<span class="product-name">'+data[i].category+' > '+ data[i].name+'</span>';if(tmsearch_reference&&data[i].reference.length)
html+='<span class="reference">'+data[i].reference+'</span>';if(tmsearch_manufacturer&&data[i].manufacturer.length)
html+='<span class="manufacturer">'+data[i].manufacturer+'</span>';if(tmsearch_description)
html+='<div class="description">'+ data[i].description_short+'</div>';if(tmsearch_price)
html+='<span class="price product-price">'+data[i].price+'</span><span class="price old-price">'+data[i].price_old+'</span>';html+='</div>';mytab[mytab.length]={data:data[i],value:html};}
return mytab;},extraParams:{ajaxSearch:1}}).result(function(event,data,formatted){$input.val(data.name);document.location.href=data.product_link;});}}
if(typeof instantsearch!='undefined'&&instantsearch){$input.on('keyup',function(){if($(this).val().length>2)
{stopInstantSearchQueries();instantSearchQuery=$.ajax({url:search_url+'?rand='+ new Date().getTime(),data:{instantSearch:1,id_lang:id_lang,q:$(this).val()},dataType:'html',type:'POST',headers:{"cache-control":"no-cache"},async:true,cache:false,success:function(data){if($input.val().length>0){tryToCloseInstantSearch();$('#center_column').attr('id','old_center_column');$('#old_center_column').after('<div id="center_column" class="'+ $('#old_center_column').attr('class')+' instant_search">'+ data+'</div>').hide();$('#slider_row').hide();ajaxCart.overrideButtonsInThePage();$("#instant_search_results a.close").on('click',function(){$input.val('');return tryToCloseInstantSearch();});return false;}
else
tryToCloseInstantSearch();}});instantSearchQueries.push(instantSearchQuery);}
else
tryToCloseInstantSearch();});}});function tryToCloseInstantSearch()
{var $oldCenterColumn=$('#old_center_column');if($oldCenterColumn.length>0)
{$('#center_column').remove();$oldCenterColumn.attr('id','center_column').show();$('#slider_row').show();return false;}}
function stopInstantSearchQueries()
{for(var i=0;i<instantSearchQueries.length;i++)
instantSearchQueries[i].abort();instantSearchQueries=[];}