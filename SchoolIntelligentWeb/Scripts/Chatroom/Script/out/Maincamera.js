$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results[1] || 0;
}
var room = $.urlParam('username');
var usertype = $.urlParam('usertype');
// create our webrtc connection
var webrtc = new SimpleWebRTC({
    // the id/element dom element that will hold "our" video
    localVideoEl: 'localVideo',
    // the id/element dom element that will hold remote videos
    remoteVideosEl: 'remotesVideos',
    // immediately ask for camera access
    autoRequestMedia: true,
    debug: false,
    detectSpeakingEvents: true
});


// when it's ready, join if we got a room from the URL
webrtc.on('readyToCall', function () {
    // you can name it anything
    if (room) webrtc.joinRoom(room);
});

var bigsize = "false";
function showVolume(el, volume) {
    if (!el) return;
    if (volume < -45) { // vary between -45 and -20
        el.style.height = '0px';
    } else if (volume > -20) {
        el.style.height = '100%';
    } else {
        el.style.height = '' + Math.floor((volume + 100) * 100 / 25 - 220) + '%';
    }
}
webrtc.on('channelMessage', function (peer, label, data) {
    if (data.type == 'volume') {
        showVolume(document.getElementById('volume_' + peer.id), data.volume);
    }
});

webrtc.on('videoAdded', function (video, peer) {
    console.log('video added', peer);
    var codejs = webrtc.getDomId(peer);
    if (codejs.indexOf("video") >= 0) {
        var remotes = document.getElementById('remotes');
        if (remotes) {
            var d = document.createElement('div');
            d.className = 'videoContainer col-md-3 col-lg-3';
            d.style = 'padding:30px;border-color:white;border-weight:10px;width:300px;height:300px;';
            d.id = 'container_' + webrtc.getDomId(peer);
            d.appendChild(video);
            var vol = document.createElement('div');
            vol.id = 'volume_' + peer.id;
            vol.className = 'volume_bar';
            if (usertype == "student") {
                webrtc.mute();
                webrtc.pauseVideo();
            }
            video.onclick = function () {
                if (bigsize == "false") {
                    video.style.width = video.videoWidth + 'px';
                    video.style.height = video.videoHeight + 'px';
                    bigsize = "true";
                }
                else if (bigsize == "true") {
                    video.style.width = '300px';
                    video.style.height = '300px';
                    bigsize = "false";
                }
            };
            d.appendChild(vol);
            remotes.appendChild(d);
        }

    }
    else if (codejs.indexOf("screen") >= 0) {
        var remotes = document.getElementById('sharescreen');
        if (remotes) {
            var d = document.createElement('div');
            d.id = 'container_' + webrtc.getDomId(peer);
            d.appendChild(video);
            remotes.appendChild(d);
            document.getElementById(d.id).setAttribute("class", "col-lg-12 col-md-12 col-sm-12 col-xs-12");
            document.getElementById(d.id).setAttribute("style", "padding:0px;");
            document.getElementById(webrtc.getDomId(peer)).setAttribute("class", "col-lg-12 col-md-12 col-sm-12 col-xs-12");
            document.getElementById(webrtc.getDomId(peer)).setAttribute("style", "padding:0px;");
        }
    }

   
});
webrtc.on('videoRemoved', function (video, peer) {
    console.log('video removed ', peer);
    var codejs = webrtc.getDomId(peer);
    if (codejs.indexOf("video") >= 0) {
        var remotes = document.getElementById('remotes');
        var el = document.getElementById('container_' + webrtc.getDomId(peer));
        if (remotes && el) {
            remotes.removeChild(el);
        }
    }
    else if (codejs.indexOf("screen") >= 0) {
        var remotes = document.getElementById('sharescreen');
        var el = document.getElementById('container_' + webrtc.getDomId(peer));
        if (remotes && el) {
            remotes.removeChild(el);
        }
        $('#sharescreen').empty();
    }
    
});

           webrtc.on('iceFailed', function (peer) {
                var connstate = document.querySelector('#container_' + webrtc.getDomId(peer) + ' .connectionstate');
                console.log('local fail', connstate);
                if (connstate) {
                    connstate.innerText = 'Connection failed.';
                    fileinput.disabled = 'disabled';
                }
            });

            // remote p2p/ice failure
            webrtc.on('connectivityError', function (peer) {
                var connstate = document.querySelector('#container_' + webrtc.getDomId(peer) + ' .connectionstate');
                console.log('remote fail', connstate);
                if (connstate) {
                    connstate.innerText = 'Connection failed.';
                    fileinput.disabled = 'disabled';
                }
            });


webrtc.on('volumeChange', function (volume, treshold) {
    //console.log('own volume', volume);
    showVolume(document.getElementById('localVolume'), volume);
});



// Since we use this twice we put it here
function setRoom(name) {
    $('form').remove();
    $('h1').text(name);
    $('#subTitle').text('Link to join: ' + location.href + "&username=" +room);
    $('body').addClass('active');
}

if (room) {
    setRoom(room);
} else {
    $('form').submit(function () {
        var val = $('#sessionInput').val().toLowerCase().replace(/\s/g, '-').replace(/[^A-Za-z0-9_\-]/g, '');
        webrtc.createRoom(val, function (err, name) {
            console.log(' create room cb', arguments);

            var newUrl = location.pathname + '?' + name;
            if (!err) {
                history.replaceState({ foo: 'bar' }, null, newUrl);
                setRoom(name);
            } else {
                console.log(err);
            }
        });
        return false;
    });
}

webrtc.on('localScreenAdded', function (video) {
    document.getElementById('localScreenContainer').appendChild(video);
    $('#localScreenContainer').show();
    document.getElementById("localScreen").setAttribute("class", "col-lg-12 col-md-12 col-sm-12 col-xs-12");
    document.getElementById("localScreen").setAttribute("style", "padding:0px;");
});


var button = $('#screenShareButton'),
    setButton = function (bool) {
        button.text(bool ? 'share screen' : 'stop sharing');
    };

   
webrtc.on('localScreenStopped', function () {

    $('#localScreenContainer').empty();
    chat.server.deletescreen($.urlParam('username'));
});

webrtc.on('localScreenRemoved', function () {
    alert("remove it");
});


webrtc.on('localScreenRemoved', function (video) {
    document.getElementById('localScreenContainer').removeChild(video);
    $('#localScreenContainer').hide();
});

// local p2p/ice failure
webrtc.on('iceFailed', function (peer) {
    var connstate = document.querySelector('#container_' + webrtc.getDomId(peer) + ' .connectionstate');
    console.log('local fail', connstate);
    if (connstate) {
        connstate.innerText = 'Connection failed.';
        fileinput.disabled = 'disabled';
    }
});

// remote p2p/ice failure
webrtc.on('connectivityError', function (peer) {
    var connstate = document.querySelector('#container_' + webrtc.getDomId(peer) + ' .connectionstate');
    console.log('remote fail', connstate);
    if (connstate) {
        connstate.innerText = 'Connection failed.';
        fileinput.disabled = 'disabled';
    }
});

function changescreen() {
    if (webrtc.getLocalScreen()) {
        $('#imgscreenpanel').empty();
        $('#imgscreenpanel').append("<img src='/Scripts/Chatroom/Script/screen-cancel.png' style='height:30px;' id='screenShareButton' class='center img-responsive' data-toggle='tooltip' title='صفحه' onclick='changescreen()'/>");
        webrtc.stopScreenShare();
    } else {
        alert("add screen");
        webrtc.shareScreen(function (err) {
            if (err) {
                $('#imgscreenpanel').empty();
                $('#imgscreenpanel').append("<img src='/Scripts/Chatroom/Script/screen.png' style='height:30px;' id='screenShareButton' class='center img-responsive' data-toggle='tooltip' title='صفحه' onclick='changescreen()'/>");
                
            } else {
                
                $('#imgscreenpanel').empty();
                $('#imgscreenpanel').append("<img src='/Scripts/Chatroom/Script/screen.png' style='height:30px;' id='screenShareButton' class='center img-responsive' data-toggle='tooltip' title='صفحه' onclick='changescreen()'/>");
            }
        });

    }
}

