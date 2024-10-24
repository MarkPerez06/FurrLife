setTimeout(function () {
    $("#subscribeModal").modal("show")
}, 2e3);

LoadBestSellerChart();
LoadMonthlyEarningChart();
LoadForcastingChart()
LoadLeastSellerChart()

function LoadBestSellerChart() {
    $.ajax({
        url: "/Dashboard/LoadBestSellerChart",
        type: 'GET',
        success: function (data) {
            var options = {
            	chart: {
                    height: 380,
                    type: "line",
                    zoom: {
                        enabled: !1
                    },
                    toolbar: {
                        show: !1
                    }
            	},
                plotOptions: {
                    stroke: {
                        width: [1, 2, 1],
                        curve: "straight",
                        dashArray: [0, 8, 5]
                    },
                },
            	dataLabels: {
            		enabled: !1
            	},
                series: data.series,
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
            	colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
            	legend: {
            		position: "bottom"
            	},
            	fill: {
            		opacity: 1
            	}
            },
                chart = new ApexCharts(document.querySelector("#line_chart_dashed"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}

function LoadLeastSellerChart() {
    $.ajax({
        url: "/Dashboard/LoadLeastSellerChart",
        type: 'GET',
        success: function (data) {
            var options = {
                chart: {
                    height: 380,
                    type: "line",
                    zoom: {
                        enabled: !1
                    },
                    toolbar: {
                        show: !1
                    }
                },
                plotOptions: {
                    stroke: {
                        width: [1, 2, 1],
                        curve: "straight",
                        dashArray: [0, 8, 5]
                    },
                },
                dataLabels: {
                    enabled: !1
                },
                series: data.series,
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                legend: {
                    position: "bottom"
                },
                fill: {
                    opacity: 1
                }
            },
                chart = new ApexCharts(document.querySelector("#line_chart_dashed1"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}
function LoadForcastingChart() {
    $.ajax({
        url: "/Dashboard/LoadForcastingChart",
        type: 'GET',
        success: function (data) {
            var options = {
                chart: {
                    height: 380,
                    type: "line",
                    zoom: {
                        enabled: !1
                    },
                    toolbar: {
                        show: !1
                    }
                },
                plotOptions: {
                    stroke: {
                        width: [1, 2, 1],
                        curve: "straight",
                        dashArray: [0, 8, 5]
                    },
                },
                dataLabels: {
                    enabled: !1
                },
                series: data,
                xaxis: {
                    categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                legend: {
                    position: "bottom"
                },
                fill: {
                    opacity: 1
                }
            },
                chart = new ApexCharts(document.querySelector("#Forecasting"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}

function LoadMonthlyEarningChart() {
    $.ajax({
        url: "/Dashboard/LoadMonthlyEarningChart",
        type: 'GET',
        success: function (data) {
            var options = {
                chart: {
                    height: 360,
                    type: "line",
                    stacked: !0,
                    toolbar: {
                        show: !1
                    },
                    zoom: {
                        enabled: !0
                    }
                },
                plotOptions: {
                    stroke: {
                        width: [1, 2, 1],
                        curve: "straight",
                        dashArray: [0, 8, 5]
                    },
                },
                dataLabels: {
                    enabled: !1
                },
                series: data.series,
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                legend: {
                    position: "bottom"
                },
                fill: {
                    opacity: 1
                }
            },
                chart = new ApexCharts(document.querySelector("#MonthlyEarningsChart"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}

//var options = {
//	chart: {
//		height: 200,
//		type: "radialBar",
//		offsetY: -10
//	},
//	plotOptions: {
//		radialBar: {
//			startAngle: -135,
//			endAngle: 135,
//			dataLabels: {
//				name: {
//					fontSize: "13px",
//					color: void 0,
//					offsetY: 60
//				},
//				value: {
//					offsetY: 22,
//					fontSize: "16px",
//					color: void 0,
//					formatter: function (e) {
//						return e + "%"
//					}
//				}
//			}
//		}
//	},
//	colors: ["#556ee6"],
//	fill: {
//		type: "gradient",
//		gradient: {
//			shade: "dark",
//			shadeIntensity: .15,
//			inverseColors: !1,
//			opacityFrom: 1,
//			opacityTo: 1,
//			stops: [0, 50, 65, 91]
//		}
//	},
//	stroke: {
//		dashArray: 4
//	},
//	series: [67],
//	labels: ["Series A"]
//};
//(chart = new ApexCharts(document.querySelector("#radialBar-chart"), options)).render();