var renderer,
    ctx,
    canvasHeight,
    canvasWidth,
    staffWidth,
    staffSpacing,
    staffNum,
    measureNum,
    toggle,
    song,
    currentPage,
    linesPerPage,
    measuresPerLine,
    cursor;

//set global variables, this must be called before using any other functions
function initComposition(canvas) {
    cursor = 0;
    currentPage = 0;
    linesPerPage = 7;
    measuresPerLine = 4;
    canvasHeight = canvas.height;
    canvasWidth = canvas.width;
    toggle = 0;
    renderer = new Vex.Flow.Renderer(canvas, Vex.Flow.Renderer.Backends.CANVAS);
    ctx = renderer.getContext();
    staffWidth = (canvas.width - 40) / 4;
    staffSpacing = 100;  //distance between staves
    handleClick(0, 0);  //selects first measure
    setInterval(function () { drawSelectHighlight(); }, 1000); //timer for measure select annimation  
}

//draw the staff lines on the canvas
function drawStaves() {
    for (line = 0; line < linesPerPage; line++) {
        for (measure = 0; measure < measuresPerLine; measure++) {
            var staff;
            if (measure == 0) {
                staff = new Vex.Flow.Stave(20, staffSpacing * line, staffWidth);
                staff.addClef("treble").setContext(ctx).draw();
            }
            else {
                staff = new Vex.Flow.Stave(20 + (staffWidth * measure), staffSpacing * line, staffWidth);
                staff.setContext(ctx).draw();
            }
            drawMeasureNotes(measure + (measuresPerLine * line), staff);
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
    measureNum = parseInt(x  / staffWidth);
    staffNum = parseInt(y / staffSpacing);
    toggle = 0;
}

//redraw staff lines, notes and anything else
function redraw() {
    drawStaves();
}

//draw each note in the model (currently expecting only one page)
function drawMeasureNotes(measureNum, staffLine)
{
    var notes = [];
    var tones, note;
    if (song.Pages.length > currentPage && song.Pages[currentPage].Measures.length > measureNum) {
        for (i = 0; i < song.Pages[currentPage].Measures[measureNum].Notes.length; i++) {
            tones = [];
            for (j = 0; j < song.Pages[currentPage].Measures[measureNum].Notes[i].Tones.length; j++) {
                tones.push(song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Key);
             }
            var beat = song.Pages[currentPage].Measures[measureNum].Notes[i].Beat;
            note = new Vex.Flow.StaveNote({ keys: tones, duration: beat });
            for (j = 0; j < song.Pages[currentPage].Measures[measureNum].Notes[i].Tones.length; j++) {
                if (song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Modifier != "")
                {
                   note.addAccidental(j, new Vex.Flow.Accidental(song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Modifier));
                }
            }
            notes.push(note);
        }
        Vex.Flow.Formatter.FormatAndDraw(ctx, staffLine, notes);
    }
}

function addNote()
{
    
}
