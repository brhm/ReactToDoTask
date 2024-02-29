import { TodoItem } from '../models/TodoItem';
import ApiService from './apiService';
import { ITodoApiResult, todoApiPromise } from '../models/ApiReponseModels';


export interface ITodoApi {
    createTodo(todo: TodoItem): Promise<ITodoApiResult<TodoItem>>;
    deleteTodo(id: number): Promise<ITodoApiResult<void>>;
    updateTodo(todo: TodoItem): Promise<ITodoApiResult<void>>;
    getTodos(): Promise<ITodoApiResult<TodoItem[]>>;
}

export const todoApiFactory = (): ITodoApi => {
    

    return {

        //async createTodo(todo: TodoItem): Promise<ITodoApiResult<TodoItem>> {
        //    try {
        //        const response = await ApiService.fetchRequest<TodoItem>('api/todoitem', {
        //            method: 'POST',
        //            headers: {
        //                'Accept': 'application/json',
        //                'Content-Type': 'application/json;charset=UTF-8'
        //            },
        //            body: JSON.stringify(todo)
        //        });

        //        return todoApiPromise('success');
        //    } catch (error) {
        //        console.error('Error creating todo:', error);
        //        return todoApiPromise('error');
        //    }
        //},

        async createTodo(todo: TodoItem) {

            try {
                const response = await fetch('api/todoitem', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json;charset=UTF-8'
                    },
                    body: JSON.stringify(todo)
                });

                if (!response.ok) {
                    return todoApiPromise('error');
                }
                
                return todoApiPromise('success');
            } catch (error) {
                console.error('Error creating todo:', error);
                return todoApiPromise('error');
            }

        },
        async deleteTodo(id: number) {
            try {
                const response = await fetch('api/todoitem/'+id, {
                    method: 'DELETE',
                });

                if (response.status >= 400) return todoApiPromise('error');
                return todoApiPromise('success');
            } catch (error) {
                console.error('Error deleting todo:', error);
                return todoApiPromise('error');
            }
        },
        async getTodos() {
            try {
                const response = await fetch('api/todoitem', {
                    method: 'GET',
                });
                if (response.status !== 200) return todoApiPromise('error');
                const data = await response.json();
                return todoApiPromise('success', data);
            } catch (error) {
                console.error('Error getting todo:', error);
                return todoApiPromise('error');
            }
        },
        async updateTodo(todo: TodoItem) {
            try {

                const response = await fetch('api/todoitem/' + todo.id, {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json;charset=UTF-8'
                    },
                    body: JSON.stringify(todo)
                });

                if (!response.ok) {
                    return todoApiPromise('error');
                }
                
                return todoApiPromise('success');
            } catch (error) {
                console.error('Error updating todo:', error);
                return todoApiPromise('error');
            }
        },
    };
};