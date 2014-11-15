using CiderHill.Catalog.Data.EF;
using CiderHill.Catalog.Data.Entities;
using System;

namespace CiderHill.Catalog.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly CatalogContext _context = new CatalogContext();
        private Repository<Category> _categoryRepository;
        private Repository<Seed> _seedRepository;
        private Repository<SeedInventory> _seedInventoryRepository;
        private Repository<SeedNote> _seedNoteRepository;
        private Repository<SeedPlanting> _seedPlantingRepository;
        private Repository<Tree> _treeRepository;
        private Repository<TreeNote> _treeNoteRepository;
        private Repository<TreePlanting> _treePlantingRepository;
        private Repository<Vendor> _vendoryRepository;

        #region Implementation of IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed()
        {
            return _disposed;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion

        public Repository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ?? (_categoryRepository = new Repository<Category>(_context));
            }
        }

        public Repository<Seed> SeedRepository
        {
            get
            {
                return _seedRepository ?? (_seedRepository = new Repository<Seed>(_context));
            }
        }

        public Repository<SeedInventory> SeedInventoryRepository
        {
            get
            {
                return _seedInventoryRepository ?? (_seedInventoryRepository = new Repository<SeedInventory>(_context));
            }
        }

        public Repository<SeedNote> SeedNoteRepository
        {
            get
            {
                return _seedNoteRepository ?? (_seedNoteRepository = new Repository<SeedNote>(_context));
            }
        }

        public Repository<SeedPlanting> SeedPlantingRepository
        {
            get
            {
                return _seedPlantingRepository ?? (_seedPlantingRepository = new Repository<SeedPlanting>(_context));
            }
        }

        public Repository<Tree> TreeRepository
        {
            get
            {
                return _treeRepository ?? (_treeRepository = new Repository<Tree>(_context));
            }
        }

        public Repository<TreeNote> TreeNoteRepository
        {
            get
            {
                return _treeNoteRepository ?? (_treeNoteRepository = new Repository<TreeNote>(_context));
            }
        }

        public Repository<TreePlanting> TreePlantingRepository
        {
            get
            {
                return _treePlantingRepository ?? (_treePlantingRepository = new Repository<TreePlanting>(_context));
            }
        }

        public Repository<Vendor> VendorRepository
        {
            get
            {
                return _vendoryRepository ?? (_vendoryRepository = new Repository<Vendor>(_context));
            }
        }

        public void Save(string author)
        {
            _context.Save(author);
        }
    }
}