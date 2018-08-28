const md5 = require("./md5")
let request = require('request');
var num = 1;
var name = "testname";
var school = "testschool";
var myMap = new Map();
var dateStr = getNowFormatDate();
let key = "testauth"
let serverurl = 'http://localhost:58158/api/values/testMD5'
myMap.set("num", num.toString());
myMap.set("name", name);
myMap.set("school", school);
myMap.set("date", dateStr);

let signStr = generateSign(myMap);
console.log(signStr);

httpRequest();

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    return currentdate;
}

function generateSign(paramMap) {
    // step 1: get all the keys
    var keyarr = [];
    paramMap.forEach(function(value, key, map) {
        keyarr.push(key);
    })

    // step2: sort the key arr
    keyarr.sort();

    // step3: traverse the key arr and combine them
    var toSignStr = "";
    keyarr.forEach(element=> {
        toSignStr = toSignStr + element;
        toSignStr = toSignStr + '=';
        toSignStr = toSignStr + paramMap.get(element);
        toSignStr = toSignStr + '&';
    });
    toSignStr = toSignStr.substr(0,toSignStr.length - 1);
    toSignStr = key + toSignStr;
    console.log(toSignStr);
    // step4: md5 the string
    return md5.hex_md5(toSignStr);
}

function httpRequest() {
    request({
        url:serverurl,
        method: "POST",
        json: true,
        headers: {
            "content-type":"application/json",
        },
        body: {
            "name":name,
            "school":school,
            "num":num,
            "sign":signStr,
            "date":dateStr,
        }
    }, (error, response, body) => {
        if(!error && response.statusCode == 200) {
            console.log(body);
        }
    })
}