﻿namespace InterviewMaster.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string QuestionsCollectionName { get; set; } = null!;
    }
}
