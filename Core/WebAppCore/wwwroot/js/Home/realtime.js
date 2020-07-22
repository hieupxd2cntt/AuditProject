"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/Hub")
    .build();

connection.on("ReceiveMessage", async function (message) {
    if ($("#txtGroup").val() =="PrivateGroup") {
        if (message == "success") {
            var sleepTime = GetWorkSecondSleep();
            if ($("#timeSleepChangeModel").length>0) {
                $("#timeSleepChangeModel").val(sleepTime);
            }
            await sleep(parseInt(sleepTime + "000"));
            $("#isNextPage").val(0);
            $("#currPage").val(1);
            LoadSop();
            //location.reload();
            //$("#form-display").submit();
        }
    }
    
});
async function sleep(milliseconds) {
    await timeout(milliseconds);
    //var start = new Date().getTime();
    //for (var i = 0; i < 1e7; i++) {
    //    var now = new Date().getTime();
    //    if ((now-start)%1000===0) {
    //        $("#timeSleepChangeModel").val(parseInt("0"+$("#timeSleepChangeModel").val())+1);
    //    }
    //    if ((now - start) > milliseconds) {
    //        break;
    //    }
    //}
}
function timeout(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


function GetWorkSecondSleep() {
    var wordSecond = 0;
    $.ajax({
        type: 'POST',
        url: $("#urlLoadWorkPlace").val(),
        data: {workplaceIdx:parseInt($("#WORKPLACE_IDX").val())},
        async: false,
        success: function (response) {
            wordSecond = response.data;
            if ($("#timeChangeModel").length > 0) {
                $("#timeChangeModel").val(response.message);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
    return wordSecond;
}

connection.on("UserConnected", function (connectionId) {
    console.log("Connected");
    if (parseInt("0" + $("#txtSuccess").val()) > 0) {
        $("#txtSuccess").val(0);
        LoadSop();
        //$(".call-realtime").click();
    }
});

connection.on("UserDisconnected", function (connectionId) {
    //var groupElement = document.getElementById("group");
    //for (var i = 0; i < groupElement.length; i++) {
    //    if (groupElement.options[i].value == connectionId) {
    //        groupElement.remove(i);
    //    }
    //}
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
$(".call-realtime").click(function () {
    connection.invoke("SendMessageToAll", "success").catch(function (err) {
            return console.error(err.toString());
        });
});
//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var message = document.getElementById("message").value;
//    var groupElement = document.getElementById("group");
//    var groupValue = groupElement.options[groupElement.selectedIndex].value;

//    if (groupValue === "All" || groupValue === "Myself") {
//        var method = groupValue === "All" ? "SendMessageToAll" : "SendMessageToCaller";
//        connection.invoke(method, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    } else if (groupValue === "PrivateGroup") {
//        connection.invoke("SendMessageToGroup", "PrivateGroup", message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    } else {
//        connection.invoke("SendMessageToUser", groupValue, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }

//    event.preventDefault();
//});

//document.getElementById("joinGroup").addEventListener("click", function (event) {
//    connection.invoke("JoinGroup", "PrivateGroup").catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});