using Microsoft.EntityFrameworkCore;
using senai_medical_group.webApi.Contexts;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalGroupContext ctx = new MedicalGroupContext();

        public void AlterarDescricao(int id, Consulta status)
        {
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            if (status.Descricao != null)
            {
                consultaBuscada.Descricao = status.Descricao;
            }

            ctx.Update(consultaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Atualiza uma consulta existente
        /// </summary>
        /// <param name="id">ID da consulta que será atualizado</param>
        /// <param name="consultaAtualizado">Objeto com as novas informações</param>
        public void Atualizar(int id, Consulta consultaAtualizada)
        {
            //busca uma consulta através do id
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            // Verifica se o ID da consulta foi informado
            if (consultaAtualizada.IdMedico != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdMedico = consultaAtualizada.IdMedico;
            }
            if (consultaAtualizada.IdPaciente != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdPaciente = consultaAtualizada.IdPaciente;
            }
            if (consultaAtualizada.IdSituacao != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdSituacao = consultaAtualizada.IdSituacao;
            }
            if (consultaAtualizada.Descricao != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.Descricao = consultaAtualizada.Descricao;
            }

            //Atualiza a classe atualizada
            ctx.Consulta.Update(consultaBuscada);

            // Salva as informações para serem gravadas no banco
            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um médico através do ID
        /// </summary>
        /// <param name="id">ID do médico que será buscado</param>
        /// <returns>Um médico buscado</returns>
        public Consulta BuscarPorId(int id)
        {
            // Retorna o primeiro tipo de evento encontrado para o ID informado
            return ctx.Consulta.FirstOrDefault(c => c.IdConsulta == id);
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoTipoEvento">Objeto novoMedico que será cadastrado</param>
        public void Cadastrar(Consulta novaConsulta)
        {
            // Adiciona este novoMedico
            ctx.Consulta.Add(novaConsulta);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um médico existente
        /// </summary>
        /// <param name="id">ID do médico que será deletado</param>
        public void Deletar(int id)
        {
            // Busca um tipo de evento através do id
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            // Remove o tipo de evento que foi buscado
            ctx.Consulta.Remove(consultaBuscada);

            // Salva as alterações
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Uma lista de médicos</returns>
        public List<Consulta> Listar()
        {
            // Retorna uma lista com todas as informações dos médicos
            return ctx.Consulta.ToList();
        }

        public List<Consulta> ListarMinhas(int id)
        {
            return ctx.Consulta
                .Include(c => c.IdMedicoNavigation)
                .Include(c => c.IdMedicoNavigation.IdEspecialidadeNavigation)
                .Include(c => c.IdMedicoNavigation.IdClinicaNavigation)
                .Where(c => c.IdMedico == id)
                .ToList();
        }

        public void Situacao(int id, string status)
        {
            Consulta consultaBuscada = ctx.Consulta
                .FirstOrDefault(p => p.IdConsulta == id);

            switch (status)
            {
                case "1":
                    consultaBuscada.IdSituacao = 1;
                    break;

                case "2":
                    consultaBuscada.IdSituacao = 2;
                    break;

                case "3":
                    consultaBuscada.IdSituacao = 3;
                    break;

                default:
                    consultaBuscada.IdSituacao = consultaBuscada.IdSituacao;
                    break;
            }

            ctx.Consulta.Update(consultaBuscada);

            ctx.SaveChanges();
        }
    }
}
