using Microsoft.EntityFrameworkCore;
using senai_InLock_webApi.Contexts;
using senai_InLock_webApi.Domains;
using senai_InLock_webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        InLockContext ctx = new InLockContext();
        public void Atualizar(int id, Estudio estudioAtualizado)
        {
            // Busca um estúdio pelo id
            Estudio estudioBuscado = ctx.Estudios.Find(id);

            // Verifica se o nome do estúdio foi informado
            if (estudioAtualizado.NomeEstudio != null)
            {
                // Atribui os novos valores aos campos existentes
                estudioBuscado.NomeEstudio = estudioAtualizado.NomeEstudio;
            }

            // Atualiza o estúdio que foi buscado
            ctx.Estudios.Update(estudioBuscado);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        public Estudio BuscarPorId(int id)
        {
            // Retorna o primeiro estúdio encontrado para o ID informado
            return ctx.Estudios.FirstOrDefault(e => e.IdEstudio == id);
        }

        public void Cadastrar(Estudio novoEstudio)
        {
            // Adiciona este novoEstudio
            ctx.Estudios.Add(novoEstudio);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            // Busca um estúdio através do seu Id
            Estudio estudioBuscado = ctx.Estudios.Find(id);

            // Remove o estúdio que foi buscado
            ctx.Estudios.Remove(estudioBuscado);

            // Salva as alterações
            ctx.SaveChanges();
        }

        public List<Estudio> Listar()
        {
            // Retorna uma lista com todas as infos dos estudios
            return ctx.Estudios.ToList();
        }

        public List<Estudio> ListarJogos()
        {
            return ctx.Estudios.Include(e =>e.Jogos).ToList();
        }

        
    }
}
