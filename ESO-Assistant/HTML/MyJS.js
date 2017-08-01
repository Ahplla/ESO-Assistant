function checkLength(time, time1, time2)
{
	if (time1.length == 0 && time2.length == 0)
		return true;
	else
	{
		var t = new Date(new Date().toDateString() + " " + time);
		var t1 = new Date(new Date().toDateString() + " " + time1);
		var t2 = new Date(new Date().toDateString() + " " + time2);
		if (t2 < t1)
			return (t1 <= t || t <= t2);
		else
			return (t1 <= t && t <= t2);
	}
}
function checkPlayer(row, name)
{
	if (name.length == 0)
		return true;
	else
		return (row.indexOf(name) != -1);
}
function checkMap(map, filter)
{
	if (filter.length == 0)
		return true;
	else
		return (map.toLowerCase() == filter);
}
function checkTeam(cell, filter)
{
	if (filter == 1)
		return (cell.getElementsByTagName("a").length == 1);
	else
		if (filter == 2)
			return (cell.getElementsByTagName("a").length > 1);
		else
			return true;
}
function checkCiv(row, filter)
{
	if (filter.length == 0)
		return true;
	else
	{
		var col1 = row.cells[1].getElementsByClassName(filter);
		var col4 = row.cells[4].getElementsByClassName(filter);
		for (var i = 0; i < col1.length; i++)
			if (col1.item(i).getElementsByClassName("myname").length == 1)
				return true;
		for (var i = 0; i < col4.length; i++)
			if (col4.item(i).getElementsByClassName("myname").length == 1)
				return true;
		return false;
	}
}
function sortlist(cl)
{
	var clTexts = new Array();
	for (i = 1; i < cl.length; i++)
	{
		clTexts[i - 1] =
			cl.options[i].text.toUpperCase() + "," +
			cl.options[i].text + "," +
			cl.options[i].value + "," +
			cl.options[i].selected;
	}
	clTexts.sort();
	for (i = 1; i < cl.length; i++)
	{
		var parts = clTexts[i - 1].split(",");
		cl.options[i].text = parts[1];
		cl.options[i].value = parts[2];
		if (parts[3] == "true")
		{
			cl.options[i].selected = true;
		}
		else
		{
			cl.options[i].selected = false;
		}
	}
}
Number.prototype.toHHMMSS = function ()
{
	var hours = Math.floor(this / 3600) < 10 ? ("00" + Math.floor(this / 3600)).slice(-2) : Math.floor(this / 3600);
	var minutes = ("00" + Math.floor((this % 3600) / 60)).slice(-2);
	var seconds = ("00" + (this % 3600) % 60).slice(-2);
	return hours + ":" + minutes + ":" + seconds;
}
function unique(arr)
{
	var u = {},
	a = [];
	for (var i = 0, l = arr.length; i < l; ++i)
	{
		if (!u.hasOwnProperty(arr[i]))
		{
			a.push(arr[i]);
			u[arr[i]] = 1;
		}
	}
	return a;
}
function setFilter(map, team, player, time1, time2, civ)
{
	var games = 0,
	wins = 0,
	times = 0;
	for (var i = 1; i < tableLength; i++)
		if (!checkMap(table.rows[i].cells[0].innerText, map) || !checkTeam(table.rows[i].cells[1], team) || !checkCiv(table.rows[i], civ) || !checkPlayer(table.rows[i].innerText, player) || !checkLength(table.rows[i].cells[7].innerText, time1, time2))
			table.rows[i].style.display = "none";
		else
		{
			var row = table.rows[i];
			games++;
			if (row.cells[1].getElementsByClassName("myname").length > 0)
				wins++;
			var a = row.cells[7].innerText.split(":");
			times += (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);
			table.rows[i].style.display = "";
		}
	var t;
	if (games != 0)
		t = ~~(times / games);
	else
		t = 0;
	var w;
	if (games != 0)
		w = wins * 100 / games;
	else
		w = 0;
	document.getElementById("fGames").innerText = games;
	document.getElementById("fWins").innerText = w.toFixed(2);
	document.getElementById("fLength").innerText = t.toHHMMSS();
	document.getElementById("rstat").style.display = "";
}

var table = document.getElementById("mytab");
var sMap = document.getElementById("mapId");
var sESO = document.getElementById("esoId");
var sTeam = document.getElementById("teamId");
var sCiv = document.getElementById("civId");
var iTime1 = document.getElementById("time1Id");
var iTime2 = document.getElementById("time2Id");
var bClear = document.getElementsByTagName("button")[0];
var countE = 1;
var esoNames = [];
var mapNames = [];
var tableLength = table.rows.length;
for (var i = 1; i < tableLength; i++)
{
	var cell1 = table.rows[i].cells[1].getElementsByTagName("a");
	for (var k = 0; k < cell1.length; k++)
	{
		esoNames[countE] = cell1.item(k).innerText;
		countE++;
	}
	var cell2 = table.rows[i].cells[4].getElementsByTagName("a");
	for (var k = 0; k < cell2.length; k++)
	{
		esoNames[countE] = cell2.item(k).innerText;
		countE++;
	}
	mapNames[i] = table.rows[i].cells[0].innerText.toLowerCase();
	var date = table.rows[i].cells[8];
	var d = new Date(date.innerText + " UTC");
	date.innerText = d.toLocaleDateString() + " " + d.toLocaleTimeString();
}
var uesoNames = unique(esoNames);
for (var k = 1; k < uesoNames.length; k++)
	sESO.options[k] = new Option(uesoNames[k], uesoNames[k]);
var umapNames = unique(mapNames);
for (var k = 1; k < umapNames.length; k++)
	sMap.options[k] = new Option(umapNames[k], umapNames[k]);
sortlist(sESO);
sortlist(sMap);
sESO.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
sMap.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
sCiv.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
sTeam.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
iTime1.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
iTime2.onchange = function ()
{
	setFilter(sMap.value, sTeam.value, sESO.value, iTime1.value, iTime2.value, sCiv.value)
};
bClear.onclick = function ()
{
	sCiv.selectedIndex = 0;
	sESO.selectedIndex = 0;
	sMap.selectedIndex = 0;
	sTeam.selectedIndex = 0;
	iTime1.value = "";
	iTime2.value = "";
	setFilter("", 0, "", "", "", "");
};
setFilter("", 0, "", "", "", "");
