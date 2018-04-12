﻿namespace RepoDb.Interfaces
{
    public interface ITrace
    {
        // Before
        void BeforeQuery(ICancellableTraceLog log);
        void BeforeUpdate(ICancellableTraceLog log);
        void BeforeDelete(ICancellableTraceLog log);
        void BeforeMerge(ICancellableTraceLog log);
        void BeforeInsert(ICancellableTraceLog log);
        void BeforeBulkInsert(ICancellableTraceLog log);
        void BeforeExecuteNonQuery(ICancellableTraceLog log);
        void BeforeExecuteReader(ICancellableTraceLog log);
        void BeforeExecuteReaderEx(ICancellableTraceLog log);
        void BeforeExecuteScalar(ICancellableTraceLog log);
        
        // After
        void AfterQuery(ITraceLog log);
        void AfterUpdate(ITraceLog log);
        void AfterDelete(ITraceLog log);
        void AfterMerge(ITraceLog log);
        void AfterInsert(ITraceLog log);
        void AfterBulkInsert(ITraceLog log);
        void AfterExecuteNonQuery(ITraceLog log);
        void AfterExecuteReader(ITraceLog log);
        void AfterExecuteReaderEx(ITraceLog log);
        void AfterExecuteScalar(ITraceLog log);
    }
}