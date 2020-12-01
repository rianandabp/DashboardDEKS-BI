

export interface ITask{
    id?: number;
    programId?: number;
    outlineId?: number;
    assignedTo?: string;
    name?: string;
    status?: boolean;
    deadline?: Date;
    progressNote?: string;
    problemNote?: string;
    lastUpdate?: Date;
}