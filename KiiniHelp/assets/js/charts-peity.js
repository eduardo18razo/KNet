$(document).ready(function() {
	'use strict';

	/* ===== Peity Inline Charts ==== */
	/* Ref: http://benpickles.github.io/peity/ */

	/* Override default settings */
	$.fn.peity.defaults.pie = {
		delimiter: null,
		fill: ["#40babd", "#f5f5f5", "#92d1d2"],
		height: null,
		radius: 16,
		width: null
	};

	$.fn.peity.defaults.donut = {
		delimiter: null,
		fill: ["#40babd", "#f5f5f5", "#92d1d2"],
		height: null,
		innerRadius: null,
		radius: 16,
		width: null
	};

	$.fn.peity.defaults.line = {
		delimiter: ",",
		fill: "#92d1d2",
		height: 32,
		max: null,
		min: 0,
		stroke: "#40babd",
		strokeWidth: 1,
		width: 64
	};

	$.fn.peity.defaults.bar = {
		delimiter: ",",
		fill: ["#40babd"],
		height: 32,
		max: null,
		min: 0,
		padding: 0.1,
		width: 64
	};

	/* Pie Charts */
	$(".peity-pie").peity("pie");

	/* Donut Charts */
	$(".peity-donut").peity("donut");

	/* Line Charts */
	$(".peity-line").peity("line");

	/* Bar Charts */
	$(".peity-bar").peity("bar");

	/* Updating Charts */
	var updatingChart = $(".peity-updating-chart").peity("line", { width: 800, height: 78 });

	setInterval(function() {
		var random = Math.round(Math.random() * 10);
		var values = updatingChart.text().split(",");
		values.shift();
		values.push(random);

		updatingChart
		.text(values.join(","))
		.change();

	}, 1000);

	/* Custom - use data attributes */
	$(".peity-pie-data-attributes").peity("pie");
	$(".peity-donut-data-attributes").peity("donut");
	$(".peity-line-data-attributes").peity("line");
	$(".peity-bar-data-attributes").peity("bar");

	/* Update chart based on percentage */
	$(window).resize(function() {
		$(".peity-full-width").peity();
	});
});
