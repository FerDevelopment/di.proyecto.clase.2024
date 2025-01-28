﻿using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Interaction logic for UCUsuario.xaml
    /// </summary>
    public partial class UCUsuario : UserControl
    {

        MVUsuario _mvCrearUsuario;
        public UCUsuario()
        {
            InitializeComponent();
        }

        public UCUsuario(MVUsuario mv)
        {
            InitializeComponent();
            _mvCrearUsuario = mv;
            DataContext = _mvCrearUsuario;

        }

        private void mItemBorrar_Click(object sender, RoutedEventArgs e)
        {
            _mvCrearUsuario.usuario = (Backend.Modelo.Usuario)dgCrearUsuario.SelectedItem;

            if (_mvCrearUsuario.borrar)
            {
                dgCrearUsuario.Items.Refresh();
                _mvCrearUsuario.usuario = new Usuario();
                MessageBox.Show("Usuario eliminado correctamente", "Gestión usuarios");
            }
            else
            {
                MessageBox.Show("Error al intentar eliminar el usuario", "Gestión usuarios");
            }
        }

        private void mItemEditar_Click(object sender, RoutedEventArgs e)
        {
            _mvCrearUsuario.usuario = (Backend.Modelo.Usuario)dgCrearUsuario.SelectedItem;

            UsuarioCrear uc = new UsuarioCrear(_mvCrearUsuario);
            uc.ShowDialog();


            if (uc.DialogResult.Equals(true))
            {
                dgCrearUsuario.Items.Refresh();
                _mvCrearUsuario.usuario = new Usuario();

            }
        }



    }
}

