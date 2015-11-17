var EditorApp = angular.module('EditorApp', []);
EditorApp.controller('CanvasController', function ($scope, $rootScope, GetSongService) {
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

    $scope.$on('addNote', function (event, args) {
        var newChord = { NoteId: args.NoteId, Tones: [], Beat: args.Beat };
        for (i = 0; i < args.Tones.length; i++) {
            newChord.Tones.push({ Key: args.Tones[i].Key, Modifier: args.Tones[i].Modifier });
        }
        var beatsInMeasure = 0;
        for (i = 0; i < $scope.song.Pages[currentPage].Measures[measureNum + (staffNum * measuresPerLine)].Notes.length; i++)
        {
            beatsInMeasure += convertBeatToNumber($scope.song.Pages[currentPage].Measures[measureNum + (staffNum * measuresPerLine)].Notes[i].Beat);
        }
        var singleBeat = $scope.song.SingleBeat;
        var totalBeatsPerMeasure = $scope.song.BeatsPerMeasure;
        if (beatsInMeasure + convertBeatToNumber(args.Beat) <= totalBeatsPerMeasure) {
            $scope.song.Pages[currentPage].Measures[measureNum + (staffNum * measuresPerLine)].Notes.push(newChord);
            drawStaves();
        }
    });
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
        measureNum = 0;
        staffNum = 0;
        blinkToggle = 0;
        setInterval(function () { drawSelectHighlight(); }, 1000); //timer for measure select annimation  
        canvas.addEventListener('click', function (event) {
            var x = event.pageX - document.getElementById('canvas_wrapper').offsetLeft - 20;
            var y = event.pageY - document.getElementById('canvas_wrapper').offsetTop;
            handleClick(x, y);
        });
    }
    //draw the staff lines on the canvas
    function drawStaves() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
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
        $rootScope.$broadcast('compositionClicked', {});
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
    function convertBeatToNumber(beat)
    {
        var beatInt = 0;
        switch (beat) {
            case 'w':
                beatInt = 1;
                break;
            case 'h':
                beatInt = 2;
                break;
            case 'q':
                beatInt = 4;
                break;
            case '8':
                beatInt = 8;
                break;
            default:
                break;
        }
        return $scope.song.SingleBeat / beatInt;
    }
});

EditorApp.factory('GetSongService', ['$http', function ($http) {
    var GetSongService = {};
    GetSongService.getSong = function () {
        return $http.get('/Editor/GetSong');
    };
    return GetSongService;
}]);

EditorApp.controller('ToolbarController', function ($scope, $rootScope, GetChordService) {

    var canvas, ctx, selectedYPos;
    getChord();

    $scope.setActive = function (type) {
        $scope.active = type;
        $scope.chord.Beat = type;
    };
    $scope.isActive = function (type) {
        return type === $scope.active;
    };
    $scope.clearChord = function () {
        $scope.chord.Tones = [];
        drawChord();
    };
    $scope.$on('compositionClicked', function (event, args) {
        $rootScope.$broadcast('addNote', $scope.chord);
    });
    function getChord() {
        GetChordService.getChord()
            .success(function (ch) {
                $scope.chord = ch;
                initChord();
            })
            .error(function (error) {
                $scope.status = 'Unable to load chord: ' + error.message;
                console.log($scope.status);
            });
    }
    function initChord()
    {
        selectedYPos = 105;
        canvas = document.getElementById("chordSelect");
        ctx = canvas.getContext("2d");
        canvas.addEventListener('click', function (event) {
            var x = event.pageX - document.getElementById('toolbar').offsetLeft;
            var y = event.pageY - document.getElementById('toolbar').offsetTop - 3;
            handleClick(x, y);
        });
        drawChord();
        if ($scope.chord.Beat == null || $scope.chord.Beat == "")
            $scope.setActive("w");
        else
            $scope.setActive($scope.chord.Beat);
    }
    function handleClick(x, y)
    {
        var toneToAdd;
        var addNote = 1;
        var normalizedYPos = normalizeYpos(y);
        if (normalizedYPos < 145 && normalizedYPos > 5) {
            selectedYPos = normalizedYPos;
            toneToAdd = { Key: translateKey(normalizedYPos), Modifier: "" };
            for (i = 0; i < $scope.chord.Tones.length; i++) {
                if ($scope.chord.Tones[i].Key == toneToAdd.Key)
                    addNote = 0;
            }
            if (addNote == 1)
                $scope.chord.Tones.push(toneToAdd);
            drawChord();
        }
    }
    function normalizeYpos(y)
    {
        var normalizedYPos;
        var offset = y % 5;
        if (offset > 2) {
            if ((y + (5 - offset)) % 5 == 0)
                normalizedYPos = y + (5 - offset);
            else
                normalizedYPos = y - (5 - offset);
        }
        else {
            if ((y + offset) % 5 == 0)
                normalizedYPos = y + offset;
            else
                normalizedYPos = y - offset;
        }
        return normalizedYPos;
    }
    function drawChord()
    {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        drawStaffLines();
        for(i = 0; i < $scope.chord.Tones.length; i++)
        {
            var yPos = translateYPos($scope.chord.Tones[i].Key);
            var xPos = 67;
            if (yPos > 110 || yPos < 60)
                drawHashMarks(yPos);
            ctx.beginPath();
            if (yPos == selectedYPos)
                ctx.fillStyle = '#ff0000';
            else
                ctx.fillStyle = '#000000';
            ctx.arc(xPos, yPos, 3, 0, 2 * Math.PI);
            ctx.fill();
            ctx.font = "12px Arial";
            if (yPos % 10 == 0)
                ctx.fillText($scope.chord.Tones[i].Key.split("/")[0] + " " + $scope.chord.Tones[i].Modifier, 105, yPos + 3);
            else
                ctx.fillText($scope.chord.Tones[i].Key.split("/")[0] + " " + $scope.chord.Tones[i].Modifier, 20, yPos + 3);
        }
    }
    function drawHashMarks(yPos) {
        var nextHash;
        if (yPos > 110) {
            nextHash = 115;
            while (nextHash <= yPos) {
                drawSingleHashMark(nextHash);
                nextHash += 10;
            }
        }
        else {
            nextHash = 55;
            while (nextHash >= yPos) {
                drawSingleHashMark(nextHash);
                nextHash -= 10;
            }
        }
    }
    function drawSingleHashMark(yPos)
    {
        ctx.beginPath();
        ctx.lineWidth = 2;
        ctx.moveTo(60, yPos);
        ctx.lineTo(76, yPos);
        ctx.strokeStyle = '#000000';
        ctx.stroke();
    }
    function drawStaffLines()
    {
        ctx.beginPath();
        ctx.lineWidth = 2;
        ctx.moveTo(50, 65);
        ctx.lineTo(85, 65);
        ctx.moveTo(50, 75);
        ctx.lineTo(85, 75);
        ctx.moveTo(50, 85);
        ctx.lineTo(85, 85);
        ctx.moveTo(50, 95);
        ctx.lineTo(85, 95);
        ctx.moveTo(50, 105);
        ctx.lineTo(85, 105);
        ctx.lineWidth = 1;
        ctx.moveTo(50, 65);
        ctx.lineTo(50, 105);
        ctx.moveTo(85, 65);
        ctx.lineTo(85, 105);
        ctx.strokeStyle = '#000000';
        ctx.stroke();
    }
    function translateYPos(key)
    {
        var yPos = 0;
        var keyElements = key.split("/");
        if (keyElements.length == 2) {
            switch (keyElements[0]) {
                case "a":
                    yPos = 90;
                    break;
                case "b":
                    yPos = 85;
                    break;
                case "c":
                    yPos = 115;
                    break;
                case "d":
                    yPos = 110;
                    break;
                case "e":
                    yPos = 105;
                    break;
                case "f":
                    yPos = 100;
                    break;
                case "g":
                    yPos = 95;
                    break;
                default:
                    yPos = 0;
                    $scope.status = 'Unable to translate key to mouse coordinates in chord designer';
                    console.log($scope.status);
                    break;
            }
            yPos += ((-(Number(keyElements[1])) + 4) * 35);
        }
        return yPos;
    }
    function translateKey(yPos)
    {
        var keyPosition = 4;
        var normalizedYPos = yPos;
        var key;
        if (yPos > 115) {
            while (normalizedYPos > 115)
            {
                normalizedYPos -= 35;
                keyPosition -= 1;
            }
        }
        else {
            while (normalizedYPos <= 80) {
                normalizedYPos += 35;
                keyPosition += 1;
            }
        }
        switch (normalizedYPos)
        {
            case 115:
                key = "c/";
                break;
            case 110:
                key = "d/";
                break;
            case 105:
                key = "e/";
                break;
            case 100:
                key = "f/";
                break;
            case 95:
                key = "g/";
                break;
            case 90:
                key = "a/";
                break;
            case 85:
                key = "b/";
                break;
            default:
                $scope.status = 'Unable to translate mouse position to a key in chord designer';
                console.log($scope.status);
                break;
        }
        key = key + keyPosition.toString();
        return key;
    }
});

EditorApp.factory('GetChordService', ['$http', function ($http) {
    var GetChordService = {};
    GetChordService.getChord = function () {
        return $http.get('/Editor/GetChord');
    };
    return GetChordService;
}]);