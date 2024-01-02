function convertImage() {
	var receberArquivo = document.getElementById('btn-send-file').files;
	var inputText = document.getElementById('btn-send-text');

	console.log(receberArquivo);

	if (receberArquivo.length > 0) {

		carregarImagem = receberArquivo[0]


		var lerArquivo = new FileReader();

		lerArquivo.onload = function (arquivoCarregado) {


			let imagemBase64 = arquivoCarregado.target.result;

			var novaImagem = document.createElement('img')
			novaImagem.src = imagemBase64;
			console.log(imagemBase64);

			inputText.value = imagemBase64
			document.getElementById('apresentar').innerHTML = novaImagem.outerHTML;
		}
		lerArquivo.readAsDataURL(carregarImagem)
	}

}