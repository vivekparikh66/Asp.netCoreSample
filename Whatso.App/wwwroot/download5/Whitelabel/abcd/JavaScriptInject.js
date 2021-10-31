(function(){var StatusCode={STALE_ELEMENT_REFERENCE:10,UNKNOWN_ERROR:13,};var NodeType={ELEMENT:1,DOCUMENT:9,};var ELEMENT_KEY='ELEMENT';var w3cEnabled=!1;var SHADOW_DOM_ENABLED=typeof ShadowRoot==='function';function generateUUID(){var array=new Uint8Array(16);window.crypto.getRandomValues(array);array[6]=0x40|(array[6]&amp;0x0f);array[8]=0x80|(array[8]&amp;0x3f);var UUID="";for(var i=0;i&lt;16;i++){var temp=array[i].toString(16);if(temp.length&lt;2)
temp="0"+temp;UUID+=temp;if(i==3||i==5||i==7||i==9)
UUID+="-"}
return UUID};function CacheWithUUID(){this.cache_={}}
CacheWithUUID.prototype={storeItem:function(item){for(var i in this.cache_){if(item==this.cache_[i])
return i}
var id=generateUUID();this.cache_[id]=item;return id},retrieveItem:function(id){var item=this.cache_[id];if(item)
return item;var error=new Error('not in cache');error.code=StatusCode.STALE_ELEMENT_REFERENCE;error.message='element is not attached to the page document';throw error},clearStale:function(){for(var id in this.cache_){var node=this.cache_[id];if(!this.isNodeReachable_(node))
delete this.cache_[id]}},isNodeReachable_:function(node){var nodeRoot=getNodeRootThroughAnyShadows(node);return(nodeRoot==document)}};function Cache(){this.cache_={};this.nextId_=1;this.idPrefix_=Math.random().toString()}
Cache.prototype={storeItem:function(item){for(var i in this.cache_){if(item==this.cache_[i])
return i}
var id=this.idPrefix_+'-'+this.nextId_;this.cache_[id]=item;this.nextId_++;return id},retrieveItem:function(id){var item=this.cache_[id];if(item)
return item;var error=new Error('not in cache');error.code=StatusCode.STALE_ELEMENT_REFERENCE;error.message='element is not attached to the page document';throw error},clearStale:function(){for(var id in this.cache_){var node=this.cache_[id];if(!this.isNodeReachable_(node))
delete this.cache_[id]}},isNodeReachable_:function(node){var nodeRoot=getNodeRootThroughAnyShadows(node);return(nodeRoot==document)}};function getNodeRoot(node){while(node&amp;&amp;node.parentNode){node=node.parentNode}
return node}
function getNodeRootThroughAnyShadows(node){var root=getNodeRoot(node);while(SHADOW_DOM_ENABLED&amp;&amp;root instanceof ShadowRoot){root=getNodeRoot(root.host)}
return root}
function getPageCache(opt_doc,opt_w3c){var doc=opt_doc||document;var w3c=opt_w3c||!1;var key='$cdc_asdjflasutopfhvcZLmcfl_';if(w3c){if(!(key in doc))
doc[key]=new CacheWithUUID();return doc[key]}else{if(!(key in doc))
doc[key]=new Cache();return doc[key]}}
function wrap(value){if((typeof(value)=='object'&amp;&amp;value!=null)||(typeof(value)=='function'&amp;&amp;value.nodeName&amp;&amp;value.nodeType==NodeType.ELEMENT)){var nodeType=value.nodeType;if(nodeType==NodeType.ELEMENT||nodeType==NodeType.DOCUMENT||(SHADOW_DOM_ENABLED&amp;&amp;value instanceof ShadowRoot)){var wrapped={};var root=getNodeRootThroughAnyShadows(value);wrapped[ELEMENT_KEY]=getPageCache(root,w3cEnabled).storeItem(value);return wrapped}
var obj;if(typeof(value.length)=='number'){obj=[];for(var i=0;i&lt;value.length;i++)
obj[i]=wrap(value[i]);}else{obj={};for(var prop in value)
obj[prop]=wrap(value[prop]);}
return obj}
return value}
function unwrap(value,cache){if(typeof(value)=='object'&amp;&amp;value!=null){if(ELEMENT_KEY in value)
return cache.retrieveItem(value[ELEMENT_KEY]);var obj;if(typeof(value.length)=='number'){obj=[];for(var i=0;i&lt;value.length;i++)
obj[i]=unwrap(value[i],cache);}else{obj={};for(var prop in value)
obj[prop]=unwrap(value[prop],cache);}
return obj}
return value}
function callFunction(shadowHostIds,func,args,w3c,opt_unwrappedReturn){if(w3c){w3cEnabled=!0;ELEMENT_KEY='element-6066-11e4-a52e-4f735466cecf'}
var cache=getPageCache(null,w3cEnabled);cache.clearStale();if(shadowHostIds&amp;&amp;SHADOW_DOM_ENABLED){for(var i=0;i&lt;shadowHostIds.length;i++){var host=cache.retrieveItem(shadowHostIds[i]);cache=getPageCache(host.webkitShadowRoot,w3cEnabled);cache.clearStale()}}
if(opt_unwrappedReturn)
return func.apply(null,unwrap(args,cache));var status=0;try{var returnValue=wrap(func.apply(null,unwrap(args,cache)))}catch(error){status=error.code||StatusCode.UNKNOWN_ERROR;var returnValue=error.message}
return{status:status,value:returnValue}};return callFunction.apply(null,arguments)}).apply(null,[null,function(){(()=&gt;{var runCount=0;var chatPanalObject=document.querySelectorAll('[class="_2zCDG"]');window.Infi=function findValueByPrefix(object,prefix)
{for(var property in object)
{if(object.hasOwnProperty(property)&amp;&amp;property.toString().startsWith(prefix))
{return object[property]}}};var reactHandler=window.Infi(chatPanalObject[0],'_reactEventHandlers');reactHandler.children.props.chat._ProxyState$state._x_id.user='{{{number}}}';function LoadContact()
{runCount ++;var renderContactList=document.querySelectorAll('[class="_1WBXd"]');if(renderContactList.length&gt;0)
{renderContactList[0].click();var ContactInfo=document.querySelectorAll('[class="_1CSx9"]');if(ContactInfo.length&gt;0)
{var closebutton=ContactInfo[0].querySelectorAll('[class="_1aTxu"]');closebutton[0].click();return!0}
else{return!1}}}
while(runCount&lt;=5&amp;&amp;!LoadContact()){}})()},[],!1])