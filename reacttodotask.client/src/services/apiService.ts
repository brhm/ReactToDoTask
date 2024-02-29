import { ITodoApiResult, todoApiPromise } from '../models/ApiReponseModels'; 

class ApiService {
    static async fetchRequest<T>(url: string, options: RequestInit): Promise<ITodoApiResult<T>> {
        try {
            const response = await fetch(url, options);

            console.log(response);
            if (!response.ok) {
                return todoApiPromise('error');
            }
            const data = await response.json();
            return todoApiPromise('success', data);
        } catch (error) {
            console.error('Error:', error);
            return todoApiPromise('error');
        }
    }
}

export default ApiService;