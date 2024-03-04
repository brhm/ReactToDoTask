import React, { useState, useEffect } from 'react';
import logo from '../assets/done_outline-black.svg';
import add from '../assets/add-black-36dp.svg';
import './styles.css';
import { TodoItem } from '../models/TodoItem';
import { todoApiFactory } from '../services/todoApi';
import { TodoItemComponent } from './TodoItem';
import { CreateTodoItemComponent } from './TodoCreate';

export const TodoList: React.FC = () => {
    const [todos, setTodos] = useState<TodoItem[]>([]);
    const [todoCreateItem, setTodoCreateItem] = useState<TodoItem>({ id: 0, completed: false, title: '', deadlineDate: new Date() });
      
    useEffect(() => {
        handleGetTodos();
    }, []);

    const handleGetTodos = async () => {
        const { getTodos } = todoApiFactory();
        getTodos().then((result) => {
            console.log(result);
            if (result.status !== 'success') {
                console.log("getTodos" + result.status);
                return;
            }
            setTodos(result.data!);
        });
    }
    const handleCreateTodo = async (createTodoItem: TodoItem) => {

        console.log("handleCreateTodo");
        console.log(createTodoItem);
        if (createTodoItem.title.length <= 10) {
            console.log("ssss")
        } else {

            const { createTodo, getTodos } = todoApiFactory();
            const response = await createTodo(createTodoItem);

            if (response.status !== 'success') return;

            handleGetTodos();

        }
    };
    const handleDeleteTodo = async (todoItem: TodoItem) => {
        const { deleteTodo, getTodos } = todoApiFactory();
        const response = await deleteTodo(todoItem.id);        

        if (response.status !== 'success') return;
        //setTodos(todos.filter((todo) => todo.id !== todoItem.id));
       
        handleGetTodos();
    };

    const handleCompleteTodo = async (todoItem: TodoItem) => {
        const { updateTodo, getTodos } = todoApiFactory();
        const response = await updateTodo(todoItem);

        if (response.status !== 'success') { 
            console.log("handleCompleteTodo : " + response.status);
            
        }
        handleGetTodos();
    };

    return (
        <div>
            <CreateTodoItemComponent
                todoItem={todoCreateItem}
                onCreate={handleCreateTodo}
            />
            
            <main className="todo-list-main">
                <ul>
                    {todos.map((todo) => (
                        <li key={todo.id}>
                            <TodoItemComponent
                                todoItem={todo}
                                onDelete={handleDeleteTodo}
                                onMarkAsCompleted={handleCompleteTodo}
                            />
                        </li>
                    ))}
                </ul>
            </main>
            
        </div>
    );
};
