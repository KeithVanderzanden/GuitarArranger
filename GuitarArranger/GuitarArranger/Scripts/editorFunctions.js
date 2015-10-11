var renderer;
var ctx;
var canvasHeight;
var canvasWidth;
var staffWidth;
var staffSpacing;
var staffNum;
var measureNum;
var toggle;

//set global variables, this must be called before using any other functions
function initComposition(canvas) {
    canvasHeight = canvas.height;
    canvasWidth = canvas.width;
    toggle = 0;
    renderer = new Vex.Flow.Renderer(canvas, Vex.Flow.Renderer.Backends.CANVAS);
    ctx = renderer.getContext();
    staffWidth = (canvas.width - 40) / 4;
    staffSpacing = 100;
    handleClick(0, 0);
    setInterval(function () { drawSelectHighlight(); }, 1000);
}

//draw the staff lines on the canvas
function drawStaves() {
    for (y = 0; y < 7; y++) {
        for (x = 0; x < 4; x++) {
            if (x == 0) {
                var staff = new Vex.Flow.Stave(20, staffSpacing * y, staffWidth);
                staff.addClef("treble").setContext(ctx).draw();
            }
            else {
                var staff = new Vex.Flow.Stave(20 + (staffWidth * x), staffSpacing * y, staffWidth);
                staff.setContext(ctx).draw();
            }
        }
    }
}

function drawSelectHighlight() {
    ctx.clearRect(0, 0, canvasWidth, canvasWidth);
    if (toggle == 0) {
        ctx.fillStyle = "#FF0000";
        ctx.fillRect(measureNum * staffWidth + 20, staffNum * staffSpacing + 40, staffWidth, staffSpacing - 60);
        ctx.fillStyle = "#000000";
        toggle = 1;
    }
    else {
        toggle = 0;
    }
    redraw();
}

//find area clicked on draw area and highlight that area
function handleClick(x, y) {
    measureNum = parseInt(x / staffWidth);
    staffNum = parseInt(y / staffSpacing);
    toggle = 0;
}

//redraw staff lines, notes and anything else
function redraw() {
    drawStaves();
}