import { RouterTypes } from '../models/RouterTypes';
import MainPage from "../pages/Public/MainPage";
import CreateMoviePage from "../pages/Admin/CreateMoviePage";
import AdminMoviesPage from '../pages/Admin/AdminMoviesPage';
import UpdateMoviePage from '../pages/Admin/UpdateMoviePage';
import AdminCinemaPage from '../pages/Admin/AdminCinemaPage';

export const publicRoutes: RouterTypes[] = [
  {path: '/main/movies', element: MainPage},
]

export const adminRoutes: RouterTypes[] = [
  {path: '/admin/movies', element: AdminMoviesPage},
  {path: '/admin/create', element: CreateMoviePage},
  {path: '/admin/update/**', element: UpdateMoviePage},
  {path: '/admin/halls', element: AdminCinemaPage},
]