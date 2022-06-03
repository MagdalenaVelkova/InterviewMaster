using System;

namespace InterviewMaster.Test.Util
{
    public abstract class TestBase : IDisposable
    {
        private bool disposed;
        protected InterviewMasterTestApp AppInTest { get; }
        protected MongoDbService MongoDbService { get; }
        public TestBase()
        {
            AppInTest = InterviewMasterTestApp.BuildApp();
            MongoDbService = AppInTest.GetService<MongoDbService>();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                TearDown();
                AppInTest?.Dispose();
                MongoDbService?.Dispose();
            }

            disposed = true;
        }

        protected virtual void TearDown() { }
    }
}
