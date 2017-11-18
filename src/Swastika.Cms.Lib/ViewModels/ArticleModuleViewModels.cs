﻿using Swastika.Cms.Lib.Models;
using Swastika.Infrastructure.Data.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using Swastika.Domain.Core.Models;
using System.Threading.Tasks;
using Swastika.IO.Cms.Lib.Models;

namespace Swastika.Cms.Lib.ViewModels
{
    public class ArticleModuleListItemViewModel : ViewModelBase<SiocCmsContext, SiocArticleModule, ArticleModuleListItemViewModel>
    {
        public string ArticleId { get; set; }
        public int ModuleId { get; set; } // ModuleId
        public string Specificulture { get; set; }
        public bool IsActived { get; set; }
        public string Description { get; set; }



        public ArticleModuleListItemViewModel(SiocArticleModule model, SiocCmsContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        #region Overrides

        #region Async

        public override Task<RepositoryResponse<bool>> RemoveRelatedModelsAsync(ArticleModuleListItemViewModel view, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            return ModuleContentViewmodel.Repository.RemoveListModelAsync(
                d => d.ArticleId == view.ArticleId && d.ModuleId == view.ModuleId && d.Specificulture == view.Specificulture);
        }

        #endregion

        #region Sync

        public override RepositoryResponse<bool> RemoveRelatedModels(ArticleModuleListItemViewModel view, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            return ModuleContentViewmodel.Repository.RemoveListModel(
                d => d.ArticleId == view.ArticleId && d.ModuleId == view.ModuleId && d.Specificulture == view.Specificulture);
        }

        #endregion

        #endregion
    }

    public class ArticleModuleFEViewModel : ViewModelBase<SiocCmsContext, SiocArticleModule, ArticleModuleFEViewModel>
    {
        public string ArticleId { get; set; }
        public int ModuleId { get; set; } // Module Id
        public string Specificulture { get; set; }
        public bool IsActived { get; set; }
        public string Description { get; set; }

        public ModuleWithDataViewModel Module { get; set; }

        public ArticleModuleFEViewModel(SiocArticleModule model, SiocCmsContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        #region Overrides        

        #region Async
        public override async Task<RepositoryResponse<bool>> SaveSubModelsAsync(SiocArticleModule parent, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            bool result = true;
            foreach (var data in Module.Data.Items)
            {
                data.ArticleId = ArticleId;
                data.ModuleId = ModuleId;
                result = result && (await data.SaveModelAsync(false, _context, _transaction)).IsSucceed;
            }
            return new RepositoryResponse<bool>()
            {
                IsSucceed = result,
                Data = result
            };

        }
        public override Task<RepositoryResponse<bool>> RemoveRelatedModelsAsync(ArticleModuleFEViewModel view, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            return ModuleContentViewmodel.Repository.RemoveListModelAsync(d => d.ArticleId == view.ArticleId
                && d.ModuleId == view.ModuleId && d.Specificulture == view.Specificulture, _context, _transaction);
        }
        #endregion

        #region Sync

        public override ArticleModuleFEViewModel ParseView(SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            var vm = base.ParseView(_context, _transaction);

            var getModuleResult = ModuleWithDataViewModel.Repository.GetSingleModel(m => m.Id == ModuleId && m.Specificulture == Specificulture);
            if (getModuleResult.IsSucceed)
            {
                vm.Module = getModuleResult.Data;
            }

            return vm;
        }

        public override  RepositoryResponse<bool> SaveSubModels(SiocArticleModule parent, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            bool result = true;
            foreach (var data in Module.Data.Items)
            {
                data.ArticleId = ArticleId;
                data.ModuleId = ModuleId;
                result = result && ( data.SaveModel(false, _context, _transaction)).IsSucceed;
            }
            return new RepositoryResponse<bool>()
            {
                IsSucceed = result,
                Data = result
            };

        }
        public override RepositoryResponse<bool> RemoveRelatedModels(ArticleModuleFEViewModel view, SiocCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            return ModuleContentViewmodel.Repository.RemoveListModel(d => d.ArticleId == view.ArticleId
                && d.ModuleId == view.ModuleId && d.Specificulture == view.Specificulture, _context, _transaction);
        }
        #endregion

        #endregion
    }
}