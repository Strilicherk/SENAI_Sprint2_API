using senai_medical_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Interfaces
{
    interface ISituacaoRepository
    {
        List<Situacao> Listar();
        Situacao BuscarPorId(int id);
        void Cadastrar(Situacao novoSituacao);
        void Atualizar(int id, Situacao situacaoAtualizado);
        void Deletar(int id);
    }
}
