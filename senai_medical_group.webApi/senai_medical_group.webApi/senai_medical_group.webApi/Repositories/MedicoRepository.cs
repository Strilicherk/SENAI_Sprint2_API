﻿using Microsoft.EntityFrameworkCore;
using senai_medical_group.webApi.Contexts;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os métodos do EF Core
        /// </summary>
        MedicalGroupContext ctx = new MedicalGroupContext();

        /// <summary>
        /// Atualiza um médico existente
        /// </summary>
        /// <param name="id">ID do médico que será atualizado</param>
        /// <param name="tipoEventoAtualizado">Objeto com as novas informações</param>
        public void Atualizar(int id, Medico medicoAtualizado)
        {
            //busca uma habilidade através do id
            Medico medicoBuscado = ctx.Medicos.Find(id);

            // Verifica se o ID da clínica foi informado
            if (medicoAtualizado.IdClinica != null)
            {
                // Atribui os novos valores aos campos existentes
                medicoBuscado.IdClinica = medicoAtualizado.IdClinica;
            }

            // Verifica se o nome do médico foi informado
            if (medicoAtualizado.NomeMedico != null)
            {
                // Atribui os novos valores aos campos existentes
                medicoBuscado.NomeMedico = medicoAtualizado.NomeMedico;
            }

            // Verifica se a especialidade do médico foi informado
            if (medicoAtualizado.IdEspecialidade != null)
            {
                // Atribui os novos valores aos campos existentes
                medicoBuscado.IdEspecialidade = medicoAtualizado.IdEspecialidade;
            }

            // Verifica se o CRM do médico foi informado
            if (medicoAtualizado.Crm != null)
            {
                // Atribui os novos valores aos campos existentes
                medicoBuscado.Crm = medicoAtualizado.Crm;
            }

            //Atualizo a classe atualizada
            ctx.Medicos.Update(medicoBuscado);

            // Salva as informações para serem gravadas no banco
            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um médico através do ID
        /// </summary>
        /// <param name="id">ID do médico que será buscado</param>
        /// <returns>Um médico buscado</returns>
        public Medico BuscarPorId(int id)
        {
            // Retorna o primeiro tipo de evento encontrado para o ID informado
            return ctx.Medicos.FirstOrDefault(m => m.IdMedico == id);
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoTipoEvento">Objeto novoMedico que será cadastrado</param>
        public void Cadastrar(Medico novoMedico)
        {
            // Adiciona este novoMedico
            ctx.Medicos.Add(novoMedico);

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
            Medico medicoBuscado = ctx.Medicos.Find(id);

            // Remove o tipo de evento que foi buscado
            ctx.Medicos.Remove(medicoBuscado);

            // Salva as alterações
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Uma lista de médicos</returns>
        public List<Medico> Listar()
        {
            // Retorna uma lista com todas as informações dos médicos
            return ctx.Medicos.ToList();
        }

        public List<Medico> ListarConsultas(int id)
        {
            var medicoBuscado = ctx.Medicos.Include(p => p.IdUsuarioNavigation)
                                               .Where(p => p.IdUsuario == id)
                                               .Select(p => new Medico()
                                               {
                                                   IdMedico = p.IdMedico,

                                                   NomeMedico = p.NomeMedico,

                                                   IdUsuarioNavigation = new Usuario()
                                                   {
                                                       IdUsuario = p.IdUsuarioNavigation.IdUsuario,

                                                       Email = p.IdUsuarioNavigation.Email
                                                   },

                                                   IdEspecialidadeNavigation = new Especialidade()
                                                   {
                                                       IdEspecialidade = p.IdEspecialidadeNavigation.IdEspecialidade,

                                                       Especialidade1 = p.IdEspecialidadeNavigation.Especialidade1
                                                   },

                                                   IdClinicaNavigation = new Clinica()
                                                   {
                                                       IdClinica = p.IdClinicaNavigation.IdClinica,

                                                       HorarioAbertura = p.IdClinicaNavigation.HorarioAbertura,

                                                       HorarioFechamento = p.IdClinicaNavigation.HorarioFechamento,

                                                       Endereco = p.IdClinicaNavigation.Endereco,

                                                       NomeFantasia = p.IdClinicaNavigation.NomeFantasia,

                                                       RazaoSocial = p.IdClinicaNavigation.RazaoSocial
                                                   }
                                               });
            return medicoBuscado.ToList();
        }
    }
}
