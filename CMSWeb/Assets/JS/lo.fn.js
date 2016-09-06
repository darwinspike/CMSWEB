var lo = {}, t, lb, $detailer, req, los = [], l = [], m = true, q = "", fl = []

,path = path || "",
url = typeof window.BranchesURL != "undefined" && BranchesURL || '../../../WebServices/GetBranch.ashx',
getLos_url = typeof window.LOSURL != "undefined" && LOSURL || '../../../WebServices/GetLoan.ashx',
  SiteID = SiteID || 0
;
if(typeof String.prototype.trim !== 'function') { String.prototype.trim = function() { return this.replace(/^\s+|\s+$/g, ''); } }
if("function"!==typeof String.prototype.removeFormat){(String.prototype.removeFormat=function(){var a=document.createElement("div");a.innerHTML=this;return a.innerText.replace(/\n\n\n/g,'').replace(/\r\r\r/g,'').replace(/\r\r/g,'').replace(/&nbsp;/g,'').replace(/\n\s/g,'').replace(/\s\n/g,'')})};

function fitBio(bio,cut){
var a,b,c;
if(bio.length>0){
a = bio.length>450 ? 
			[( (bio.indexOf('\n')>450&&bio.indexOf('\n')<500)||bio.indexOf('\n',400)>400
				? bio.substring( 0, (bio.indexOf('\n',200)>150
										? bio.indexOf('\n',200)
										: bio.indexOf('\n'))+1 )
				: bio.substring( 0, 450 ) )
			, ( (bio.indexOf('\n')>450&&bio.indexOf('\n')<500)||bio.indexOf('\n',400)>400
				? bio.substring( (bio.indexOf('\n',200)>150
								? bio.indexOf('\n',200)
								: bio.indexOf('\n'))+1, bio.length )
				: bio.substring( 450, bio.length ) ), bio] 
		: [( bio.indexOf('\n')>100&&(bio.indexOf('\n')>((bio.length/2)-50)&&bio.indexOf('\n')<((bio.length/2)+50))||bio.indexOf('\n',(bio.length/2))>(bio.length/2)
				? bio.substring( 0, ( bio.indexOf('\n',( (bio.length/2)-50) )>(bio.length/2)
					? bio.indexOf('\n',( (bio.length/2)-50) )
					: bio.indexOf('\n',(bio.length/2) ) ) )
				: bio.substring( 0, (bio.length/2) )  )
			, ( bio.indexOf('\n')> (bio.length/2) 
				? bio.substring( bio.indexOf('\n')+1, bio.length )
				: bio.substring( (bio.length/2), bio.length ) ), bio];
c = cut!=undefined?cut:false;
b = [a[0].replace(/(\r\n|\n|\r)/gm,"<br />").replace(/\s+/g," "), a[1].replace(/(\r\n|\n|\r)/gm,"<br />").replace(/\s+/g," "), bio.replace(/(\r\n|\n|\r)/gm,"<br />").replace(/\s+/g," ")];
if(c) b[1] = b[1].length>350?b[1].substring(0,350)+'&hellip;':b[1];
$detailer.find('.lo-bio').eq(0).html(b[0]+'&mdash;');
$detailer.find('.lo-bio').eq(1).html(b[1]);
}else{
$detailer.find('[rel="readmore"]').remove();
$detailer.find('.lo-bio').eq(0).html('<p>The loan officer doesn\'t have biography.</p>');
}
return a;
}

lo = {
_d: {
    _l: getLos_url + '?SiteID=' + SiteID,
	_w: {
		h: window.innerHeight,
		w: window.innerWidth
	},
	open: false,
	detailer: "",
	html: "",
	_p: function(p){
		clearTimeout(t);
		if(p==0){$('.lo-progressbarseek').show().css({width: 0});}
		else if(p<0){$('.lo-progressbarseek').hide();}
		else if(p>0&&p<100){$('.lo-progressbarseek').show().css({width:p+'%'});}
		else{$('.lo-progressbarseek').show(); t=setTimeout(function(){
			$('.lo-progressbarseek').css({width: (100>=parseInt(p)?p:100) + '%'});
			t=setTimeout(function(){lo._d._p(-1);},400);
		},400);}
	}
},
init: function(){
$(".lo-container select").chosen({width: "100%", disable_search_threshold: 1000});
$detailer = $('<div class="detailer-container" />');
$detailer.html(lo._d.detailer);

lo.search()

$('body').on('click', '.lo-officer>a', function(){
	if($(this).closest('.lo-officer').hasClass('active')){
		lo.closeCards();
	}else{
		lo.openCard($(this));
	}
}).on('click', '.lo-container [rel="close"]', function(){
	lo.closeCards();
}).on('click', '.lo-container [rel="readmore"]', function(){
	fitBio(lb.bio);
	$(this).remove();
}).on('click', '.reset-search', function () {
    $('#query').val("");
    $('#lo-filter').val(0).trigger('chosen:updated').trigger('change');
}).on('change', '#lo-filter', function () {
var thisVar = parseInt($(this).val());
switch(true){
	case thisVar==0:
		thisVar=false;
		break;
	case thisVar==1:
		thisVar=true;
		break;
}

if(!thisVar){
	m=true;
	$('body').removeClass('search');
	$('.lo-results-list').html('');
	lo.showlos(12);
}else{
	m=false;
	$('body').addClass('search');
	$('#query').focus();
	$('[rel="moreresults"]').hide();
	$('.lo-results-list').html('<p>Start typing to search.</p>');
}

}).on('keyup', '#query', function(){
$('[rel="moreresults"]').hide();
$('.lo-results-list').html('');
lo.filterResults();
});
},
filterResults: function(){
lo.closeCards();

var a,b;
b=$('#query').val();q=b.toLowerCase();
fl=[];
for(i=0; i<los.length; i++){
if(los[i].fullName.toLowerCase().indexOf(q)>-1){
fl.push(los[i]);
}
}

lo.showlos(12);

},
isMobile: function(){return/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase())||767>window.innerWidth?!0:!1},
isSmallMobile: function(){return window.innerWidth<481},
update: function(){
lo._d._w={h: window.innerHeight,w: window.innerWidth}
if(lb!=undefined&&lo._d.open) lo.openDetailer(lb);
return lo;
},
getInfo: function(card){
var dataContainer = card.find('.lo-officer-data'),
	name = dataContainer.find('[name="fullname"]').val(),
	title = dataContainer.find('[name="title"]').val(),
	address = dataContainer.find('[name="address"]').val(),
	bio = dataContainer.find('[name="bio"]').val().removeFormat(),
	nmls = dataContainer.find('[name="nmls"]').val(),
	nmls = dataContainer.find('[name="nmls"]').val(),
	phone = dataContainer.find('[name="phone"]').val(),
	email = dataContainer.find('[name="email"]').val(),
	website = dataContainer.find('[name="website"]').val(),
	index = card.closest('.lo-officer-item').index(),
	humanIndex = (index+1),
	totalCards = $('.lo-officer-item').length;
	return {fullname:name,title:title,address:address,nmls:nmls,phone:phone,email:email,website:website,bio:bio,a:{
		a:index,b:humanIndex,c:totalCards
	}};
},
openDetailer: function(info){
lo.closeCards();
lo._d.open=true;
lo._d._p(0);
lb=info;
$('body').addClass('viewlo');
$('.lo-officer-item').eq(lb.a.a).find('.lo-officer').addClass('active');

		$detailer = $('<div class="detailer-container" />');
		$detailer.html(lo._d.detailer
			.replace(/{{name}}/g,info.fullname)
			.replace(/{{title}}/g,info.title)
			.replace(/{{address}}/g,info.address)
			.replace(/{{nmls}}/g,info.nmls.length>0&&info.nmls!=null?'NMLS#'+info.nmls:"")
			.replace(/{{phone}}/g,info.phone)
			.replace(/{{website}}/g, ( info.website.length>0 ? '<a href="' + (info.website.indexOf('http')>-1 ?"":"//") + info.website +  '">Website</a> &bull;' : ''  ) )
			.replace(/{{email}}/g, ( info.website.length>0 ? '<a href="mailto:' + info.email +  '">Contact</a>' : ''  ) )
			);

var a=false;
fitBio(info.bio, true);

if(lo.isSmallMobile()){

$('.lo-officer-item').eq(info.a.a).after($detailer);
console.log('after',info.a.a);

}else if(lo.isMobile()){

switch(true){
case info.a.a==0 && info.a.c==1:
	$('.lo-officer-item').eq(info.a.a).after($detailer);
	console.log('after first',info.a.a);
	a=true;
	break;
case info.a.a==info.a.c:
	$('.lo-officer-item').eq(info.a.a).after($detailer);
	console.log('after last',info.a.a);		
	a=true;	
	break;	
case info.a.b%2==1:
	if($('.lo-officer-item').eq(info.a.a+1).length>0){
		$('.lo-officer-item').eq(info.a.a+1).after($detailer);
		console.log('after',info.a.a+1);
		a=true;
	}else{
		$('.lo-officer-item').eq(info.a.a).after($detailer);
		console.log('after',info.a.a);
		a=true;
	}
	break;
case info.a.b%2==0:
	$('.lo-officer-item').eq(info.a.a).after($detailer);
	console.log('after',info.a.a);
	a=true;
	break;
}

}else{
// else
switch(true){
case info.a.b%3==1 && info.a.a<info.a.c:
	if($('.lo-officer-item').eq(info.a.a+2).length>0){
		$('.lo-officer-item').eq(info.a.a+2).after($detailer);
		console.log('after',info.a.a+2);
		a=true;
	}else{
		if($('.lo-officer-item').eq(info.a.a+1).length>0){
			$('.lo-officer-item').eq(info.a.a+1).after($detailer);
			console.log('after',info.a.a+1);
			a=true;
		}else{
			$('.lo-officer-item').eq(info.a.a).after($detailer);
			console.log('after',info.a.a);
			a=true;
		}
	}
	break;
case info.a.b%3==2 && info.a.a<info.a.c:
	if($('.lo-officer-item').eq(info.a.a+1).length>0){
		$('.lo-officer-item').eq(info.a.a+1).after($detailer);
		console.log('after',info.a.a+1);
		a=true;
	}else{
		$('.lo-officer-item').eq(info.a.a).after($detailer);
		console.log('after',info.a.a);
		a=true;
	}
	break;
case info.a.b%3==0 && info.a.a<info.a.c:
	$('.lo-officer-item').eq(info.a.a).after($detailer);
	console.log('after',info.a.a);
	a=true;
	break;
case info.a.a==0 && info.a.c==1:
	$('.lo-officer-item').eq(info.a.a).after($detailer);
	console.log('after first',info.a.a);
	a=true;
	break;
case info.a.b==info.a.c||info.a.a==info.a.c:
	$('.lo-officer-item').eq(info.a.c-1).after($detailer);
	console.log('after last',info.a.a);		
	a=true;	
	break;	
}

// /else

}

lo._d._p(100);
return true;
},
closeCards: function(){$('body').removeClass('viewlo');$('.lo-officer').removeClass('active');$('.detailer-container').remove();lo._d.open=false;},
openCard: function(card){
lo.closeCards();
var info = lo.getInfo(card);
lo.openDetailer(info);
return info;
},
showlos: function(limit){
if(m){
	var g = "", f = $('.lo-officer-item').length;
	l = limit<los.length?los.slice(f,limit+f):los;
}else{
	var g = "", f = 0; $('[rel="moreresults"]').hide();
	l = limit<fl.length?fl.slice(f,limit+f):fl;
}

if(l.length>(limit-1)&&m) $('[rel="moreresults"]').show();
else $('[rel="moreresults"]').hide();

for(i=0; i<l.length;i++){
	var a = $('<div />');
		a.html(lo._d.html);
	a.html( a.html().replace(/{{name}}/g,l[i].fullName)
	.replace(/{{title}}/g,l[i].position)
	.replace(/{{phone}}/g,l[i].phone)
	.replace(/{{nmls}}/g,l[i].nmls)
	.replace(/{{addressline}}/g,l[i].address)
	.replace(/{{city}}/g,l[i].city)
	.replace(/{{state}}/g,l[i].state)
	.replace(/{{bio}}/g,l[i].biography.removeFormat())
	.replace(/{{zipcode}}/g,l[i].zipcode)
	.replace(/{{id}}/g,l[i].id)
	.replace(/{{address}}/g,l[i].address+' '+l[i].city+' '+l[i].state+' '+l[i].zipcode)
	.replace(/{{email}}/g,(l[i].email!=null?l[i].email:""))
	.replace(/{{website}}/g,(l[i].website!=null?l[i].website:"")) );
	g+=a.html();
}
console.log(f,limit+f,l);
//end for
if(l.length==0){
	$('.lo-results-list').html('<p>No loan officers found.</p>');
}else{
	$('.lo-results-list').append(g);
}
},
clearLos: function(){
$('.lo-results-list').empty();
},
search: function(){
var a,b,c;
a=$('#lo-filter').val();b=$('#query').val();c=lo._d._l+(b.length>0?"&name="+b:"");
if(req!=undefined&&req!=null){
	req.abort();
}
req=$.ajax({
url: c,
success: function(xhr){
los=xhr;
if(xhr.length>0 && xhr.status==undefined){
lo.clearLos();
lo.showlos(12);
}else{
$('.lo-results-list').html('<p>No loan officers found.</p>');
}
},
 xhr: function() {
        var xhr = new window.XMLHttpRequest();
       xhr.addEventListener("progress", function(evt) {
           if (evt.lengthComputable) {
               var percentComplete = evt.loaded / evt.total;
               //Do something with download progress
               percentComplete = parseInt(percentComplete*100);
               lo._d._p(percentComplete);
               $('.lo-results-list').html('<h3 style="text-align: center; padding: 20px">' + percentComplete + '%</h3>');
           }
       }, false);
       return xhr;
    }
});

}

}


$(document).on('ready', function(){
lo.init();
}).ajaxStart(function(){
lo._d._p(10);
}).ajaxComplete(function(){
lo._d._p(100);
});

$(window).on('resize', function(){
lo.update();
});