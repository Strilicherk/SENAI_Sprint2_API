using senai_medical_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Interfaces
{
    interface IConsultaRepository
    {

        List<Consulta> Listar();

        Consulta BuscarPorId(int id);

        void Cadastrar(Consulta novaConsulta);

        void Atualizar(int id, Consulta consultaAtualizada);

        void Deletar(int id);

        List<Consulta> ListarMinhas(int id);

        void AlterarDescricao(int id, Consulta status);

        void Situacao(int id, string status);
    }
}
