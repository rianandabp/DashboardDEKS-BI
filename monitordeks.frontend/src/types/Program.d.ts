

export interface IProgram{
    id?: number;
    name?: string;
    description?: string;
    progressPercentage?: number;
    totalTask?:number;
    taskComplete?:number;
    startDate?: Date;
    endDate?: Date;
    lastUpdate?: Date;
} 
