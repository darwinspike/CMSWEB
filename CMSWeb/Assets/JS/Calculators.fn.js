console.log(path);
$(document).ready(function(){
    console.log(path);
$('#calcs_body').load(path + 'Calculator1');

var toggleBtnBusy = false;

$('#calcbtn').on('click',function(){
	if(toggleBtnBusy==false){
		$('#calcs_menu').slideToggle();
	}
});

$('#calcs_menu a').on('click',function(){
	$('#calcbtn').text('wait...');
	$('body').animate({opacity: 0.8}, 500);
	toggleBtnBusy=true;
	var idx = $(this).attr('data-calc');
	var newText = $(this).text();
	$(this).addClass('active').parent('li').siblings('li').children('a').removeClass('active');
	$.ajax({
	    url: path + 'Calculator' + idx + '',
		success: function(htmlResponse){
			$('#calcs_body').html(htmlResponse);
			toggleBtnBusy=false;
			$('#calcbtn').html('<i class="list-icon"></i> More Calculators');
			$('#calcs_menu').slideUp();
			setTimeout(function(){
				$('body').animate({opacity: 1}, 500);
				$('.calcs_title').text(newText);
			},500);
		}
	}).error(function(){
		$('#calcbtn').text('something bad..');
		toggleBtnBusy=true;
		setTimeout(function(){
			toggleBtnBusy=false;
			$('#calcbtn').html('<i class="list-icon"></i> More Calculators');
			$('body').animate({opacity: 1}, 500);
		},1500);
	});
});

// .fadeTo(500,.5)

$(window).on('scroll', function(){
  if($(window).scrollTop()>200){
    $('.calc_lo.fixed').stop(true,false).animate({top: $(window).scrollTop()+10, opacity: 0.9},500);
  }else{
    $('.calc_lo.fixed').stop(true,false).animate({top: $(window).scrollTop()+10, opacity: 0},500);
  }
});

});


function floor(number)
{
     return Math.floor(number*Math.pow(10,2))/Math.pow(10,2);
}

function dosumMortgageCalc()
{
    var calc = $("#calcMortgage");
    
    var _IR = calc.find("#IR");//
    var _YR = calc.find("#YR"); //    
    var _YRvalue = _YR.val();
    var _PI = calc.find("#PI"); //
    var _MT = calc.find("#MT"); //
    var _MI = calc.find("#MI"); //
    var _LA = calc.find("#LA"); //
    var _AT = calc.find("#AT"); //
    var _AI = calc.find("#AI"); //
    var _MP = calc.find("#MP"); // 

     var mi = _IR.val() / 1200;
     var base = 1;
     var mbase = 1 + mi;

     for (i=0; i< _YRvalue * 12; i++)
     {
          base = base * mbase
     }

     _PI.val(floor(_LA.val() * mi / (1 - (1 / base))));
     _MT.val(floor(_AT.val() / 12));
     _MI.val(floor(_AI.val() / 12));

     var dasum = _LA.val() * mi / (1 - (1 / base)) + _AT.val() / 12 + _AI.val() / 12;

     _MP.val(floor(dasum));
}


















function checkInput(input, min, max, msg)
{
  msg = msg + " field has invalid data: " + input.value;
  var lField = ltrim(rtrim(String(input.value)));
    myeareg=new RegExp("^[$]?[0-9]*\\.?[0-9]");
  if (!(myeareg.test(lField) && lField!='.'))
  {
    alert("Only numeric values are allowed!");
    input.focus();
    input.select();
    return false;
  }
  var num = parseFloat(input.value);
  if (num < min || max < num)
  {
    alert(msg + " not in range [" + min + ".." + max + "]");
    return false;
  }
    return true;
}
function rtrim(argvalue) {
  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {
  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}
// Round a field two (2) decimals
function round(number)
{
  return Math.round(number*Math.pow(10,2))/Math.pow(10,2);
}

function trim(str)
{
     return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}

function dosumIncome()
{
  var calc = $("#calcIncome");
  var form = { 
    mortAmt: { value: calc.find("#mortAmt").val() },  
    numYears: { value: calc.find("#numYears").val() },  
    propTax: { value: calc.find("#propTax").val() },  
    debt: { value: calc.find("#debt").val() },  
    mortRate: { value: calc.find("#mortRate").val() } 
  };
   
   if (( trim(form.mortAmt.value) != "" && trim(form.numYears.value) != "" && trim(form.propTax.value) != "" &&
        trim(form.debt.value) != "" && trim(form.mortRate.value) != ""))
  {
    var tmp1,tmp2, tmp3, tmp4, tmp5, tmp6 ,tmp7, tmp8, tmp9, tmp10, tmp11, tmp12, tmp13, tmp14, tmp15

    tmp1 = parseFloat(form.mortAmt.value);
    if (isNaN(tmp1)) tmp1=0;
    tmp2 = parseFloat(form.numYears.value);
    if (isNaN(tmp2)) tmp2=0;
    tmp3 = parseFloat(form.mortRate.value);
    if (isNaN(tmp3)) tmp3=0;
    tmp4 = parseFloat(form.propTax.value);
    if (isNaN(tmp4)) tmp4=0;
    tmp5 = parseFloat(form.debt.value);
    if (isNaN(tmp5)) tmp5=0;
    tmp6 = 0;

     tmp7 = tmp4/12;
     tmp8= tmp5;
      tmp9 = tmp3/1200;
      tmp10= tmp2 * 12;
      tmp11 = (1 + tmp9);
     for (i=1; i < tmp10; i++) {
        tmp11 = tmp11 * (1 + tmp9);
     }
      tmp12  = tmp1 * tmp11 * tmp9 / (tmp11 - 1);
      tmp13 = tmp12 + tmp7 + tmp8;
      tmp14= (tmp12 + tmp7) / 0.28;
      tmp15 = tmp13 / 0.35;

     if (tmp14 > tmp15) {
      tmp6 = 12 * tmp14;
     } else {
      tmp6 = 12 * tmp15;
    }
     
    calc.find("#mortPay").val(round(tmp12));
    calc.find("#totalPay").val(round(tmp13));
    calc.find("#reqdSal").val(round(tmp6));
  }
  }
  



















  	if (!document.all) {
		document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
	}


function MyCheckEnteredValue(element) {
	var lField = ltrim(rtrim(String(element.value)));
    myReg=new RegExp("^[$]?[0-9]*\\.?[0-9]*$"); 
	if (!(myReg.test(lField) && lField!='.' && lField!='$')) {
		alert("Only numeric values are allowed!");
		element.focus();
		element.select();
		return false;
	}
	return true;
}

function MyCheckEnteredValuePercent(element) {
	var lField = ltrim(rtrim(String(element.value)));
    myReg=new RegExp("^[0-9]*\\.?[0-9]*?$"); 
	if (!(myReg.test(lField) && lField!='.' )) {
		alert("Only numeric values are allowed ");
		return false;
	}

	//100% test
	if (parseFloat(lField)>100) {
		alert("Percentage values must be between 0 and 100!")
		return false;
	}
	return true;
}

function rtrim(argvalue) {
  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {
  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}

function round(number)
{
  return Math.round(number*Math.pow(10,2))/Math.pow(10,2);
}



function dosumHowMuchPayments()
{   frm = $("#calcHowMuchPayments");
	var mi = frm.find("#IR").val() / 1200;  
	var counter = 1;  
	var mcounter = 1 + mi;  
	for (i=0; i<frm.find("#YR").val() * 12; i++)  
	{   
		counter = counter * mcounter  
	}  
	frm.find("#PI").val(round(frm.find("#LA").val() * mi / ( 1 - (1/counter))));  
	frm.find("#MT").val(round(frm.find("#AT").val() / 12));
	frm.find("#MI").val(round(frm.find("#AI").val() / 12));
	var dasum = frm.find("#LA").val() * mi / (1 - (1 / counter)) + frm.find("#AT").val() / 12 + frm.find("#AI").val() / 12;
   frm.find("#MP").val(round(dasum));
}












  if (!document.all) {
    document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
  }


function MyCheckEnteredValue(element) {
  var lField = ltrim(rtrim(String(element.value)));
    myReg=new RegExp("^[$]?[0-9]*\\.?[0-9]*$"); 
  if (!(myReg.test(lField) && lField!='.' && lField!='$')) {
    alert("Only numeric values are allowed!");
    element.focus();
    element.select();
    return false;
  }
  return true;
}

function MyCheckEnteredValuePercent(element) {
  var lField = ltrim(rtrim(String(element.value)));
    myReg=new RegExp("^[0-9]*\\.?[0-9]*?$"); 
  if (!(myReg.test(lField) && lField!='.' )) {
    alert("Only numeric values are allowed ");
    return false;
  }

  //100% test
  if (parseFloat(lField)>100) {
    alert("Percentage values must be between 0 and 100!")
    return false;
  }
  return true;
}

function rtrim(argvalue) {
  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {
  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}

function round(number)
{
  return Math.round(number*Math.pow(10,2))/Math.pow(10,2);
}



function dosumHowMuchPayments()
{   frm = $("#calcHowMuchPayments");
  var mi = frm.find("#IR").val() / 1200;  
  var counter = 1;  
  var mcounter = 1 + mi;  
  for (i=0; i<frm.find("#YR").val() * 12; i++)  
  {   
    counter = counter * mcounter  
  }  
  frm.find("#PI").val(round(frm.find("#LA").val() * mi / ( 1 - (1/counter))));  
  frm.find("#MT").val(round(frm.find("#AT").val() / 12));
  frm.find("#MI").val(round(frm.find("#AI").val() / 12));
  var dasum = frm.find("#LA").val() * mi / (1 - (1 / counter)) + frm.find("#AT").val() / 12 + frm.find("#AI").val() / 12;
   frm.find("#MP").val(round(dasum));
}















function MyCheckEnteredValue(element) {
	var lField = ltrim(rtrim(String(element.value)));

    myReg=new RegExp("^[0-9]*\\.?[0-9]*$"); 
        if (!(myReg.test(lField) && lField!='.')) {
			alert("Please enter valid numerical data in all fields!");
			return false;
		}

	element.value=lField; 
	return true;
}


function rtrim(argvalue) {

  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {

  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}

 function trim(str)
{
     return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}  

function dosumHowMuchBorrow() 
{
	var tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8, tmp9, tmp10, tmp11
	var calc = $("#calcHowMuchBorrow");
	var form = {
		CondoMaintenanceFee: { value: calc.find("#CondoMaintenanceFee").val() },
		income: { value: calc.find("#income").val() },
		other: { value: calc.find("#other").val() },
		taxes: { value: calc.find("#taxes").val() },
		insurance: { value: calc.find("#insurance").val() },
		auto: { value: calc.find("#auto").val() },
		cards: { value: calc.find("#cards").val() },
		term: { value: calc.find("#term").val() },
		rate: { value: calc.find("#rate").val() },
		payment: { value: calc.find("#payment").val() }
	};

  var CondoMaintenanceFee = 0.0;
  if( trim(form.CondoMaintenanceFee.value) != "" ){ 
    CondoMaintenanceFee = parseFloat(form.CondoMaintenanceFee.value);
    if (isNaN(CondoMaintenanceFee)) CondoMaintenanceFee=0;
  }
  
  if ( trim(form.income.value) != "" &&  trim(form.other.value) != "" &&  trim(form.taxes.value) != "" &&  trim(form.insurance.value) != "" &&  trim(form.auto.value) != "" &&  trim(form.cards.value) != "" &&  trim(form.term.value) != "" &&  trim(form.rate.value) != ""){
    									 
    tmp12 = Math.round(eval(form.income.value * .28) + eval(form.other.value * .28) - form.taxes.value - form.insurance.value - CondoMaintenanceFee);

    tmp13 = Math.round(eval(form.income.value * .36) + eval(form.other.value * .36) - form.taxes.value -  form.insurance.value - form.auto.value - form.cards.value -CondoMaintenanceFee);

      if (tmp12>tmp13) {
      form.payment.value = tmp13
      }
      else {
      form.payment.value = tmp12
      }

    tmp1 = parseFloat(form.rate.value);
	if (isNaN(tmp1)) tmp1=0;
    tmp2 = parseFloat(form.term.value);
	if (isNaN(tmp2)) tmp2=0;
    tmp3 = parseFloat(form.payment.value);
	if (isNaN(tmp3)) tmp3=0;
    tmp4 = parseFloat(tmp1 / 1200); 
    tmp5 = parseFloat(tmp2 * 12);
    tmp6 = parseFloat(1 + tmp4);
    tmp7 = parseFloat(Math.pow(tmp6, tmp5));
    tmp8 = parseFloat(1 / tmp7);
    tmp9 = parseFloat(1 - tmp8);
    tmp10 = parseFloat(tmp9 / tmp4);
    tmp11 = parseFloat(tmp3 * tmp10);
    
    calc.find("#amount").val(Math.round(tmp11));
    calc.find("#payment").val(form.payment.value);

    }
else
	{
		alert("Please enter valid numerical data in all fields!");
		return false;
	}
}

function newWindow() {     window.open("explain.html","Window4","toolbar=0,location=0,directories=0,status=0,menubar=1,scrollbars=1,resizable=0,copyhistory=0,width=500,height=250");    }















	if (!document.all) {
		document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
	}

function checkInput(input, min, max, msg)
{   
	msg = msg + " field has invalid data: " + input.value;
	var lField = ltrim(rtrim(String(input.value)));
    myReg=new RegExp("^[$]?[0-9]*\\.?[0-9]"); 
	if (!(myReg.test(lField) && lField!='.'))
	{          
		alert(msg);
		input.focus();
		input.select();
		return false;
	}   	
	var num = parseFloat(input.value);
	if (num < min || max < num)
	{        
		alert(msg + " not in range [" + min + ".." + max + "]"); 
		return false;    
	}      
		return true;
}
function rtrim(argvalue) {
  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {
  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}
// Round a field two (2) decimals
function round(number)	// no decimals
{
  return Math.round(number*Math.pow(10,2))/Math.pow(10,2);
}
function trim(str)
{
     return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}

function calculateForm(form)
{
if ((trim(form.find("#payments").val())!= "") && (trim(form.find("#interest").val())!= "") && (trim(form.find("#principal").val())!= ""))
{
	   var i = form.find("#interest").val();   
			if (i > 1.0) 
				{       
					i = i / 100.0;        
					form.find("#interest").val(i);    
				}   
			i /= 12;   
			var counter = 1;  
			for (var j = 0; j < form.find("#payments").val(); j++)  
				counter = counter * (1 + i);
                form.find("#payment").val(round(round(form.find("#principal").val() * counter * i) / (counter - 1)));
                form.find("#totalint").val(round((form.find("#payment").val() * form.find("#payments").val()) - form.find("#principal").val()));
		}
	}






















	if (!document.all) {
		document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
	}


function dosumHowMuchDownPayment()
{
	var frm = document.all;	
	var tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8, tmp9, tmp10
	tmp1 = parseFloat(frm.home.value);
	
	if (isNaN(tmp1)) tmp1=0;
	tmp2 = parseFloat(frm.dp_percent.value);
	if (isNaN(tmp2)) tmp2=0;

	tmp3 = parseFloat(frm.rate.value);
	
	if (isNaN(tmp3)) tmp3=0;
	tmp4 = parseFloat(frm.close_points.value);
	if (isNaN(tmp4)) tmp4=0;
	tmp5 = parseFloat(frm.term.options[frm.term.selectedIndex].value);
	
	if (isNaN(tmp5)) tmp5=0;
	tmp6 = parseFloat(frm.tax.value);
		
	if (isNaN(tmp6)) tmp6=0;
	tmp7 = parseFloat(frm.insurance.value);

	if (isNaN(tmp7)) tmp7=0;
	frm.mortgage.value = round(tmp1 - ((tmp2 / 100) * tmp1));
	
	tmp8 = parseFloat(frm.mortgage.value);
		
	if (isNaN(tmp8)) tmp8=0;
	frm.dp_amount.value = round(tmp1 * (tmp2 / 100));
	
	tmp9 = parseFloat(frm.dp_amount.value);
	if (isNaN(tmp9)) tmp9=0;
	frm.mo_tax.value = round(tmp6 / 12);
	
	tmp10 = parseFloat(frm.mo_tax.value);
	if (isNaN(tmp10)) tmp10=0;
	frm.mo_ins.value = round(tmp7 / 12);
	
	tmp11 = parseFloat(frm.mo_ins.value);
	if (isNaN(tmp11)) tmp11=0;
	frm.points.value = round((tmp4 / 100) * tmp8);
	
	frm.close_cost.value = round(990 + (.01 * tmp8));
		
	frm.prepaids.value = round(((.015 * tmp8) * .8333) + (.0035 * tmp8));
		
		
	frm.mo_pmt.value = round(getPayment(tmp8, tmp3, tmp5));
		
	frm.totPmt.value = round(eval(frm.mo_pmt.value) + tmp10 + tmp11);
	frm.totalCash.value = round(tmp9 + eval(frm.points.value) + eval(frm.close_cost.value) + eval(frm.prepaids.value));
	

	frm.apr.value = getAPR(tmp5, tmp3, tmp8, frm.mo_pmt.value);
		
}
	

	
function getPayment(amount, rate, term)
{
		
	rate = rate / 100;

	var payment = (amount * rate) / (1 - Math.pow((1 + (rate/12)), (-1 * (term*12))));

	return payment/12;

}


	
function oldTerm(rate, bal, mo_pmt) 
{
		
	var ppy = 12;
	rate = rate / 100;
		
	monthsLeft = -1 * (Math.log(1-(bal/mo_pmt)*(rate/ppy)))/Math.log(1+(rate/ppy));
		
	return monthsLeft;

}

function getAPR (term, rate, amount, pmt) 
{
	 
	months = term * 12;
	  
	if (rate > 1) rate = rate / 100; 
	tmp1 = Math.round(((.01 * amount) + 375 + (((amount * rate) / 365) * 30))*100)/100;
	
	tmp2 = rate / 12;
	 
	tmp3 = Math.round((amount - tmp1) * 100) / 100;
	  
	cmpd = Math.pow( (1 + tmp2) , (-1 * months) );
	  
	apr = ((pmt * (1 - (cmpd) )) / tmp3) * 12 * 100;
	  
	apr = Math.round(apr * 1000) / 1000;
	  
	return apr;
	
}

function MyCheckEnteredValue(element) {
	var lField = ltrim(rtrim(String(element.value)));
    myReg=new RegExp("^[$]?[0-9]*\\.?[0-9]"); 
	if (!(myReg.test(lField) && lField!='.')) {
		alert("Only numeric values are allowed");
		return false;
	}
	return true;
}
function rtrim(argvalue) {

  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {

  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}



// Round a field two (2) decimals
function round(number)	// no decimals
{
  return Math.round(number*Math.pow(10,2))/Math.pow(10,2);
}


















if (!document.all) {
	document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
}

function checkInput(input, min, max, msg)
{   
	msg = msg + " field has invalid data: " + input.value;
	var lField = ltrim(rtrim(String(input.value)));
    myReg=new RegExp("^[$]?[0-9]*\\.?[0-9]"); 
	if (!(myReg.test(lField) && lField!='.'))
	{          
		alert(msg);
		input.focus();
		input.select();
		return false;
	}   	
	var num = parseFloat(input.value);
	if (num < min || max < num)
	{        
		alert(msg + " not in range [" + min + ".." + max + "]"); 
		input.focus();
		input.select();
		return false;
		
	}      
		return true;
}
function rtrim(argvalue) {
  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}
function ltrim(argvalue) {
  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}
function trim(str)
{
     return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}

function computeForm(form) {

    var term = form.find("#term").val();
    var interest = form.find("#interest").val();
    var principal = form.find("#principal").val();
    
    if ((trim(term)!= "") && (trim(interest)!= "") && (trim(principal)!= "")) {
        var payments = term * 12;
        var tmp1, tmp2
        tmp1 = interest;
          
        if (tmp1 > 1.0)
        {
	        tmp1 = tmp1 / 100.0;
	        interest = tmp1;
        }
           
        tmp1 /= 12;
        tmp2 = 1;
         
        for (var counter = 0; counter < payments; counter++) tmp2 = tmp2 * (1 + tmp1);       
	   form.find("#payment").val((principal * tmp2 * tmp1) / (tmp2 - 1));
    }
}


























	if (!document.all) {
		document.constructor.prototype.__defineGetter__('all', function() { return document.getElementsByTagName('*'); });
	}


// Mortgage calculator code
function floor(number)
{
     return Math.floor(number*Math.pow(10,2))/Math.pow(10,2);
}

function dosumInterest()
{

    var amnt=0;
    var rate=0;
    var years=0;
    var pmnt=0;
    var pmntl=0;
    var pmntnum=0;

    amnt= eval(document.all.ioc_amnt.value);
    rate = eval (document.all.ioc_rate.value);
    years = eval (document.all.ioc_years.value);
     
    pmnt=(rate/1200)*amnt;
    pmntl=pmnt*years*12;
    pmntnum=years*12;
    pmnt=pmnt*100;pmnt=(Math.floor(pmnt))/100;
    pmntl=pmntl*100;pmntl=(Math.floor(pmntl))/100;
    
    document.all.MonthlyPayment.value=" "+pmnt;//Monthly Payment
    document.all.TotalOfPayments.value=" "+pmntl;//Total of All Payments
    document.all.NumberOfPayments.value=" "+pmntnum;//Number of Payments
}
function MyCheckEnteredValue(element) {
	var lField = ltrim(rtrim(String(element.value)));

    myReg=new RegExp("^[0-9]*\\.?[0-9]*$"); 
        if (!(myReg.test(lField) && lField!='.')) {
			alert("Please enter valid numerical data in all fields!");
			return false;
		}

	element.value=lField; 
	return true;
}


function rtrim(argvalue) {

  while (1) {
    if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
      break;
    argvalue = argvalue.substring(0, argvalue.length - 1);
  }
  return argvalue;
}

function ltrim(argvalue) {

  while (1) {
    if (argvalue.substring(0, 1) != " ")
      break;
    argvalue = argvalue.substring(1, argvalue.length);
  }
    return argvalue;
}

 function trim(str)
{
     return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}  
