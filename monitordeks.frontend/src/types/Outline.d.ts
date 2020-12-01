
export interface IOutline{
    id?: number;
    name?: string;
    ProgramId?: number;
    progressPercentage?: number;
    totalTask?:number;
    taskComplete?:number;
    deadline?: Date;
    progressNote?: string;
    problemNote?: string;
    lastUpdate?: Date;
} 