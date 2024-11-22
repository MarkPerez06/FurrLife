setTimeout(function () {
    $("#subscribeModal").modal("show")
}, 2e3);

LoadPetHealthRecordChartWithPredictions();
LoadAppointmentsChartWithPredictions();
LoadMonthlyEarningChartWithPredictions();
LoadBestSellerChart();
LoadMonthlyEarningChart();
LoadForcastingChart()
LoadLeastSellerChart()

function LoadPetHealthRecordChartWithPredictions() {
    $.ajax({
        url: "/Dashboard/LoadPetHealthRecordChartWithPredictions",
        type: 'GET',
        success: function (data) {
            var options = {
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
                }
            },
                chart = new ApexCharts(document.querySelector("#LoadPetHealthRecordChartWithPredictions"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}

function LoadAppointmentsChartWithPredictions() {
    $.ajax({
        url: "/Dashboard/LoadAppointmentsChartWithPredictions",
        type: 'GET',
        success: function (data) {
            var options = {
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
                }
            },
                chart = new ApexCharts(document.querySelector("#LoadAppointmentsChartWithPredictions"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}


function LoadMonthlyEarningChartWithPredictions() {
    $.ajax({
        url: "/Dashboard/LoadMonthlyEarningChartWithPredictions",
        type: 'GET',
        success: function (data) {
            var options = {
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
                }
            },
                chart = new ApexCharts(document.querySelector("#LoadMonthlyEarningChartWithPredictions"), options);
            chart.render();
        },
        error: function () {
            console.log('Failed to load the partial view');
        }
    });
}

function LoadBestSellerChart() {
    $.ajax({
        url: "/Dashboard/LoadBestSellerChart",
        type: 'GET',
        success: function (data) {
            var options = {
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#556ee6", "#34c38f", "#ff0000", "#b200ff", "#f1b44c"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
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
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#d35400", "#8e44ad", "#1abc9c", "#e74c3c", "#3498db"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
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
                series: data,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#FF6F61", "#4CAF50", "#FFEB3B", "#42A5F5", "#FF9800", "#AB47BC", "#26C6DA", "#EC407A", "#e74c3c", "#16a085"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
                }
            },
                chart = new ApexCharts(document.querySelector("#Forecasting"), options);
            chart.render();

            //var options = {
            //    series: data.series,
            //    chart: {
            //        height: 350,
            //        type: "area",
            //        toolbar: {
            //            show: !1
            //        }
            //    },
            //    colors: ["#ff5733", "#c70039", "#900c3f", "#581845", "#28b463"],
            //    dataLabels: {
            //        enabled: !1
            //    },
            //    stroke: {
            //        curve: "smooth",
            //        width: 2
            //    },
            //    fill: {
            //        type: "gradient",
            //        gradient: {
            //            shadeIntensity: 1,
            //            inverseColors: !1,
            //            opacityFrom: .45,
            //            opacityTo: .05,
            //            stops: [20, 100, 100, 100]
            //        }
            //    },
            //    xaxis: {
            //        categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
            //    },
            //    markers: {
            //        size: 3,
            //        strokeWidth: 3,
            //        hover: {
            //            size: 4,
            //            sizeOffset: 2
            //        }
            //    },
            //    legend: {
            //        position: "top",
            //        horizontalAlign: "right"
            //    }
            //},
            //    chart = new ApexCharts(document.querySelector("#Forecasting"), options);
            //chart.render();
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
                series: data.series,
                chart: {
                    height: 350,
                    type: "area",
                    toolbar: {
                        show: !1
                    }
                },
                colors: ["#2ecc71", "#f39c12", "#9b59b6", "#e74c3c", "#16a085"],
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    curve: "smooth",
                    width: 2
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        inverseColors: !1,
                        opacityFrom: .45,
                        opacityTo: .05,
                        stops: [20, 100, 100, 100]
                    }
                },
                xaxis: {
                    categories: ["Jan " + data.year, "Feb " + data.year, "Mar " + data.year, "Apr " + data.year, "May " + data.year, "Jun " + data.year, "Jul " + data.year, "Aug " + data.year, "Sep " + data.year, "Oct " + data.year, "Nov " + data.year, "Dec " + data.year]
                },
                markers: {
                    size: 3,
                    strokeWidth: 3,
                    hover: {
                        size: 4,
                        sizeOffset: 2
                    }
                },
                legend: {
                    position: "top",
                    horizontalAlign: "right"
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