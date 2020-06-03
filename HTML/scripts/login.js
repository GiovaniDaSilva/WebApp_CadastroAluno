(() => {
    if (sessionStorage.getItem('token' != null)){
        window.location.href = "index.html";
    }
})()

var login = function(){
    event.preventDefault();

    var nome = document.querySelector('#email');
    var senha = document.querySelector('#password');


    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://localhost:44329/token', true);

    xhr.onload = function(){
        var resultado = JSON.parse(this.responseText);        
        console.log(resultado);
        if (resultado.error != 'invalid_grant') {
            sessionStorage.setItem('token', `${resultado.token_type} ${resultado.access_token}`)
            sessionStorage.setItem('username', `${resultado.UserName}`)
            verificar();
        } else {
            alert(resultado.error_description);
            nome.value = '';
            senha.value = '';
        }        
    }

    xhr.setRequestHeader('content-type', 'application/x-www-form-urlencoded')
    xhr.send(`grant_type=password&username=${nome.value}&password=${senha.value}`);
    
  }

var verificar = function(){
    var xhr = new XMLHttpRequest();

    xhr.open('GET', `https://localhost:44329/api/Aluno/RecuperarDB`, true);

    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'))

    xhr.onerror = function () {
        console.log('ERROR', xhr.readyState);
    }
  
    xhr.onreadystatechange = function () {
       var result = this.responseText;
       window.location.href="index.html";
    }

    xhr.send();
}
