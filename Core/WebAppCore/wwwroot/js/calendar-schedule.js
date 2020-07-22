
$(document).ready(function () {
	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();

	/*  className colors

	className: default(transparent), important(red), chill(pink), success(green), info(blue)

	*/


	/* initialize the external events
	-----------------------------------------------------------------*/

	$('#external-events div.external-event').each(function () {

		// create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
		// it doesn't need to have a start or end
		var eventObject = {
			title: $.trim($(this).text()) // use the element's text as the event title
		};

		// store the Event Object in the DOM element so we can get to it later
		$(this).data('eventObject', eventObject);

		// make the event draggable using jQuery UI
		$(this).draggable({
			zIndex: 999,
			revert: true,      // will cause the event to go back to its
			revertDuration: 0  //  original position after the drag
		});

	});


	/* initialize the calendar
	-----------------------------------------------------------------*/
	$('.calendar-schedule').each(function () {
		var id = $(this).attr("id");
		var calendar = $("#" + id).fullCalendar({
			header: {
				left: 'title',
				center: 'agendaDay,agendaWeek,month',
				right: 'prev,next today'
			},
			editable: true,
			firstDay: 1, //  1(Monday) this can be changed to 0(Sunday) for the USA system
			selectable: true,
			defaultView: 'month',

			axisFormat: 'h:mm',
			columnFormat: {
				month: 'ddd',    // Mon
				week: 'ddd d', // Mon 7
				day: 'dddd M/d',  // Monday 9/7
				agendaDay: 'dddd d'
			},
			titleFormat: {
				month: 'MMMM yyyy', // September 2009
				week: "MMMM yyyy", // September 2009
				day: 'MMMM yyyy'                  // Tuesday, Sep 8, 2009
			},
			allDaySlot: false,
			selectHelper: true,		
		
			select: function (start, end, allDay) {
				var title = prompt('Event Title:');
				if (title) {
					calendar.fullCalendar('renderEvent',
						{
							title: title,
							start: start,
							end: end,
							allDay: allDay
						},
						true // make the event "stick"
					);
				}
				calendar.fullCalendar('unselect');
			},
			events: JSON.parse($("#calendar-schedule-" + id).val()),
			eventClick: function (info) {
				RunEventClick(info.modId);
				//info.el.style.borderColor = 'red';
			},
		});
	});
	


});

function RunEventClick(sModId) {
	$.ajax({
		type: 'POST',
		url: $("#urlAjaxLoadModSchedule").val(),
		data: jQuery.param({ modId: sModId}),
		async: false,
		success: function (response) {
			debugger;
			$("#modal-calendar-schedule").modal("show");
			$("#body-schedule").html(response);;
			//if (response.resultCode == undefined) {
			//	bootbox.alert("Bạn không có quyền thực hiện chức năng này");
			//	return;
			//}
			//if (parseInt(response.resultCode) > 0) {
				
			//	//if (response.data == "success") {
			//	//	bootbox.alert("Xóa dữ liệu thành công", function () {
			//	//		location.reload();
			//	//	});
			//	//}
			//	//else {
			//	//	bootbox.alert(response.data);
			//	//}
			//}
			//else {
			//	//alert(response.messeage);
			//}
		},
		error: function (error) {
			console.log(error);
			rs = - 1;
		}
	});
}