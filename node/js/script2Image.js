var Canvas = require('canvas'),
	fs = require('fs');

fs.readFile('jquery.js', 'utf8', function (err,data) {
	if (err) {
		return console.log(err);
	}

	var stringData = data,
		dataLen = stringData.length,
		width = Math.ceil(Math.sqrt(dataLen)),
		height = width,
		canvas = new Canvas(width, height),
		ctx = canvas.getContext('2d')
		asciiValue = 32,
		imgData = ctx.getImageData(0,0,width,height),
		data = imgData.data, 
		len = data.length;

		for(var i=0, index=0, asciiValue;i<len;i=i+4){
			asciiValue = 32			
			if(index<dataLen){
				asciiValue = stringData[index].charCodeAt(0);
				console.log(asciiValue)
				if(asciiValue <0 && asciiValue > 255){
					asciiValue=32;
				}				
				index++;
			}
			data[i] = asciiValue;
			data[i+1] = asciiValue;
			data[i+2] = asciiValue;
			data[i+3] = 255;
		}
		ctx.putImageData(imgData, 0, 0);

		var out = fs.createWriteStream(__dirname + '/text.png'),
		 	stream = canvas.pngStream();
		stream.on('data', function(chunk){
		  out.write(chunk);
		});

		stream.on('end', function(){
		  console.log('saved png');
		});
	});



