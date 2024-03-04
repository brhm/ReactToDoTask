export interface TodoItem {
    id: number;
    title: string;
    completed: boolean;
    createdDate?: Date;
    completeDate?: Date;
    deadlineDate: Date;
}
