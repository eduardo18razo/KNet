$(document).ready(function() {
	'use strict';

	/* ===== jQuery Steps ==== */
	/* Ref: https://github.com/rstaib/jquery-steps */
	$("#example-basic").steps({
		headerTag: "h2",
		bodyTag: "section",
		transitionEffect: "slideLeft",
		titleTemplate: '<span class="number">#index#</span> #title#<span class="step-arrow"></span>',
		autoFocus: true
	});

	$("#example-vertical").steps({
		headerTag: "h3",
		bodyTag: "section",
		transitionEffect: "slideLeft",
		titleTemplate: '<span class="number">#index#</span> #title#<span class="step-arrow"></span>',
		stepsOrientation: "vertical"
	});
});
