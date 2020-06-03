var tbody = document.querySelector('table tbody');
var _aluno = {};


function Cadastrar() {

    _aluno.nome = document.querySelector('#nome').value;
    _aluno.sobrenome = document.querySelector('#sobrenome').value;
    _aluno.telefone = document.querySelector('#telefone').value;
    _aluno.ra = document.querySelector('#ra').value;

    console.log(_aluno)

    if (_aluno.id === undefined || _aluno.id === 0) {
        SalvarEstudantes('POST', 0, _aluno)
    } else {
        SalvarEstudantes('PUT', _aluno.id, _aluno)
    }

    CarregaEstudantes();
    $('#myModal').modal('hide')
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#tituloModal');

    var _nome = document.querySelector('#nome');
    var _sobrenome = document.querySelector('#sobrenome');
    var _telefone = document.querySelector('#telefone');
    var _ra = document.querySelector('#ra');


    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Aluno";


    _nome.value = '';
    _sobrenome.value = '';
    _telefone.value = '';
    _ra.value = '';

    _aluno = {};
    console.log(_aluno);

    $('#myModal').modal('hide')
}

function CarregaEstudantes() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open('GET', `https://localhost:44329/api/Aluno/RecuperarDB`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'))
    xhr.onerror = function () {
        console.log('ERROR', xhr.readyState);
    }

    /* 
     xhr.onload = function () {
         var estudantes = JSON.parse(this.responseText);
         for (var indice in estudantes) {
             AdicionaLinha(estudantes[indice])
         }
     }
     */
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var estudantes = JSON.parse(this.responseText);
                for (var indice in estudantes) {
                    AdicionaLinha(estudantes[indice])
                }
            }else if (this.status == 500){
                var erro = JSON.parse(this.responseText);
                console.log(erro);
            }
        }
    }

    xhr.send();
}

function SalvarEstudantes(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0) {
        id = '';
    }

    xhr.open(metodo, `https://localhost:44329/api/Aluno/${id}`, false);
    xhr.setRequestHeader('content-type', 'application/json')
    xhr.send(JSON.stringify(corpo));

}

CarregaEstudantes();

function AdicionaLinha(estudante) {
    var trow = `<tr>
                    <td>${estudante.nome}</td> 
                    <td>${estudante.sobrenome}</td>
                    <td>${estudante.telefone}</td>
                    <td>${estudante.ra}</td>
                    <td><button  class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='EditarEstudante(${JSON.stringify(estudante)})'>Editar</button>
                        <button  class="btn btn-danger"  onclick='DeletarEstudante(${JSON.stringify(estudante)})'>Deletar</button></td>
                </tr>
                `
    tbody.innerHTML += trow;

}

function EditarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#tituloModal');

    var _nome = document.querySelector('#nome');
    var _sobrenome = document.querySelector('#sobrenome');
    var _telefone = document.querySelector('#telefone');
    var _ra = document.querySelector('#ra');


    btnSalvar.textContent = "Salvar";
    titulo.textContent = `Editar Aluno ${estudante.nome}`;

    _nome.value = estudante.nome;
    _sobrenome.value = estudante.sobrenome;
    _telefone.value = estudante.telefone;
    _ra.value = estudante.ra;

    _aluno = estudante;
    console.log(_aluno)
}

function DeletarEstudante(estudante) {
    var excluir = Boolean;

    bootbox.confirm({
        message: `Deseja excluir o estudante ${estudante.nome}?`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                var xhr = new XMLHttpRequest();
                xhr.open('Delete', `https://localhost:44329/api/Aluno/${estudante.id}`, true);
                xhr.onload = function () {
                    CarregaEstudantes();
                }
                xhr.send();
            }
        }
    });
}


function NovoAluno() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#tituloModal');

    var _nome = document.querySelector('#nome');
    var _sobrenome = document.querySelector('#sobrenome');
    var _telefone = document.querySelector('#telefone');
    var _ra = document.querySelector('#ra');


    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Aluno";


    _nome.value = '';
    _sobrenome.value = '';
    _telefone.value = '';
    _ra.value = '';

    _aluno = {};
    console.log(_aluno);

    $('#myModal').modal('show')
}


(() => {
    var statusLogin = document.querySelector('#statusLogin');
    var menu = document.querySelector('#menu');

    if (sessionStorage.getItem('token') != null){
        
        menu.innerHTML = `<li class="active"><a href="#">Aluno</a></li>
                             <li class=""><a href="#">Disciplina</a></li>`    
        
        statusLogin.innerHTML =  `<li class="dropdown">
        <a href="#" class="dropdown-toggle"
            data-toggle="dropdown" role="button"
            aria-haspopup="true"
            aria-expanded="false">${sessionStorage.getItem('username')} <span
                class="caret"></span></a>
        <ul class="dropdown-menu">
            <li><a href="#">Perfil</a></li>                                
            <li><a href="#">Configuração</a></li>
            <li role="separator" class="divider"></li>                                
            <li><a href="#" onClick="logout()"">Sair</a></li>
        </ul>
    </li>`;
    }else{
        statusLogin.innerHTML =  `<li><a href="login.html">Entrar</a></li>` ;
        window.location.href = "login.html";
    }
})()

function logout(){
    sessionStorage.removeItem('token');
    window.location.href = "login.html";
}

