var EditorApp = angular.module('EditorApp', []);
EditorApp.controller('CanvasController', function ($scope, GetSongService) {
    //Editor variables
    var renderer,
    ctx,
    staffWidth,
    staffSpacing,
    staffNum,
    measureNum,
    blinkToggle,
    currentPage,
    linesPerPage,
    measuresPerLine,
    cursor;

    initComposition();
    getSong();
   
    function getSong() {
        GetSongService.getSong()
            .success(function (s) {
                $scope.song = s;
                drawStaves();
            })
            .error(function (error) {
                $scope.status = 'Unable to load song: ' + error.message;
                console.log($scope.status);
            });
    }

    function initComposition() {
        canvas = document.getElementById('composition');
        cursor = 0;
        currentPage = 0;
        linesPerPage = 7;
        measuresPerLine = 4;
        blinkToggle = 0;
        renderer = new Vex.Flow.Renderer(canvas, Vex.Flow.Renderer.Backends.CANVAS);
        ctx = renderer.getContext();
        staffWidth = (canvas.width - 40) / 4;
        staffSpacing = 100;  //distance between staves
        handleClick(0, 0);  //selects first measure
        setInterval(function () { drawSelectHighlight(); }, 1000); //timer for measure select annimation  
        canvas.addEventListener('click', function (event) {
            var x = event.pageX - document.getElementById('canvas_wrapper').offsetLeft - 20;
            var y = event.pageY - document.getElementById('canvas_wrapper').offsetTop;
            handleClick(x, y);
        });
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
        ctx.clearRect(0, 0, canvas.width, canvas.width);
        if (blinkToggle == 0) {
            ctx.fillStyle = "#FF0000";
            ctx.fillRect(measureNum * staffWidth + 20, staffNum * staffSpacing + 40, staffWidth, staffSpacing - 60);
            ctx.fillStyle = "#000000";
            blinkToggle = 1;
        }
        else {
            blinkToggle = 0;
        }
        drawStaves();
    }

    //find area clicked on draw area and highlight that area
    function handleClick(x, y) {
        measureNum = parseInt(x / staffWidth);
        staffNum = parseInt(y / staffSpacing);
        blinkToggle = 0;
    }

    //draw each note in the model (currently expecting only one page)
    function drawMeasureNotes(measureNum, staffLine) {
        var notes = [];
        var tones, note;
        if ($scope.song.Pages.length > currentPage && $scope.song.Pages[currentPage].Measures.length > measureNum) {
            for (i = 0; i < $scope.song.Pages[currentPage].Measures[measureNum].Notes.length; i++) {
                tones = [];
                for (j = 0; j < $scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Tones.length; j++) {
                    tones.push($scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Key);
                }
                var beat = $scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Beat;
                note = new Vex.Flow.StaveNote({ keys: tones, duration: beat });
                for (j = 0; j < $scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Tones.length; j++) {
                    if ($scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Modifier != "") {
                        note.addAccidental(j, new Vex.Flow.Accidental($scope.song.Pages[currentPage].Measures[measureNum].Notes[i].Tones[j].Modifier));
                    }
                }
                notes.push(note);
            }
            Vex.Flow.Formatter.FormatAndDraw(ctx, staffLine, notes);
        }
    }
});

EditorApp.factory('GetSongService', ['$http', function ($http) {
    var GetSongService = {};
    GetSongService.getSong = function () {
        return $http.get('/Editor/GetSong');
    };
    return GetSongService;
}]);
