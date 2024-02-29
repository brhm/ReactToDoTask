export type ResultStatus =
    | 'success'
    | 'error'
    | 'not found';
 
export interface ITodoApiResult<T> {
    status: ResultStatus;
    data?: T;
}

export const todoApiPromise = <T>(
    status: ResultStatus,
    data?: T
): ITodoApiResult<T> => ({
    status,
    data,
});