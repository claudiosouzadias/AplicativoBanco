using Microsoft.Win32;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Encapsulamento_06._06._2024
{
    class Program
    {
        static void Main(string[]args)
        {
             Console.WriteLine("Hello World!");
             //Criando objeto profile da class USER que permite chamar metodos dela.
             User profile = new User();

             //Variaveis de controle de loop
             bool registro = false;
             bool login = false;
             bool interf = false;



             //Comecando o codigo
             inicio();

             void registrar(){
                 registro = true;
                 while (registro)
                 {
                     //Variavel pra verificar se a senha é a mesma
                     bool verifica = true;

                     //Variavel pra continuar o loop de registro
                     bool continua = true;
                     string registra_user;
                     string registra_senha;

                     while (true)
                     {
                         //Registrando as informacoes do usuario
                         Console.WriteLine("Por favor insira o seu nome de usuario");
                         string register_user = Console.ReadLine()!;
                         if (!string.IsNullOrEmpty(register_user))
                         {
                             registra_user = register_user;
                             Console.WriteLine("Por favor insira a senha");
                             string register_senha = Console.ReadLine()!;
                             if (!string.IsNullOrEmpty(register_senha))
                             {
                                 registra_senha = register_senha;
                                 break;
                             } else { Console.WriteLine("Senha invalida"); }
                         } else { Console.WriteLine("Usuario invalido"); }

                     }

                     //Looping de exeucao final de registro
                     while (continua)
                     {
                         //Confirmando a senha
                         Console.WriteLine("Por favor confirme a sua senha 2 vezes");
                         string confirma_1 = Console.ReadLine();
                         Console.WriteLine("Por favor confirme novamente");
                         string confirma_2 = Console.ReadLine();

                         //Verificando a confirmacao 
                         if (confirma_1 != confirma_2
                             || confirma_1 != registra_senha
                             || confirma_2 != registra_senha)
                         {
                             Console.WriteLine("As senhas não são iguais");
                         }
                         else
                         {
                             //Executando o registro 
                             while (verifica)
                             {
                                 Console.WriteLine("Digite 1 para confirmar o registro");
                                 string input = Console.ReadLine();

                                 //Verificando se o numero digitado é valido
                                 if (input != "1")
                                 {
                                     Console.WriteLine("Ensira um numero valido");
                                 }
                                 else
                                 {
                                     Console.Clear();

                                     //Atribuindo o valor de registro a conta
                                     profile.user = registra_user;
                                     profile.senha = registra_senha;

                                     //Confirmando a senha do usuario
                                     Console.WriteLine($"Estes é seu usuario: {profile.user} e esta é a sua senha: {profile.senha}?");
                                     Console.Write("Digite 1 para confirmar e 2 para repetir o processo!");
                                     string input_confirma = Console.ReadLine();

                                    if (input_confirma == "1")
                                     {
                                        adicionando();
                                        Thread.Sleep(2000);
                                        //Mandando o usuario a tela de interface
                                        interfaceUsuario();
                                        verifica = false;
                                        continua = false;
                                        registro = false;

                                    } else if (input_confirma == "2")
                                     {
                                         //Repetindo o loop
                                         verifica = false;
                                         continua = false;
                                     }
                                 }
                             }
                         }
                     }
                 }
             }

             void logar()
             {
                 login = true;
                 while (login)
                 {
                     Console.WriteLine("Por favor ensira seu nome de usuario e senha!");
                     Console.WriteLine("Usuario:");
                     string input_1 =  Console.ReadLine();
                     Console.WriteLine("Senha:");
                     string input_2 = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input_1) && !string.IsNullOrEmpty(input_2))
                    {
                        string compara = input_1 + input_2;

                        for(int i = 0; i < profile.users.GetLength(0);) 
                        { 
                            string combina = profile.users[i,0] + profile.users[i,1];

                            if (combina == compara)
                            {
                                profile.atualUser = i;
                                login = false;
                                interfaceUsuario();
                            }
                            else { Console.WriteLine("Usuario ou senha invalidos"); }
                        }
                    }
                    else { Console.WriteLine("Insira valores validos!"); }

                    /* if(input_1 == profile.user) 
                     {
                         if (input_2 == profile.senha)
                         {
                             login = false;
                             interfaceUsuario();

                         } else { Console.WriteLine("Senha incorreta!"); }

                     } else { Console.WriteLine("Usuario invalido"); }*/



                 }
             }

             void inicio()
             {
                 Console.Clear();

                profile.senha = null;
                profile.user = null;

                 Console.WriteLine("Seja bem-vindo ao banco souza, faca o login ou o registro e desfrute de nossos servicos!");
                 Console.WriteLine("1 - Näo sou um cliente (Executar registro)"); Console.WriteLine("2 - Ja sou um cliente (Executar login)");
                 string input = Console.ReadLine();
                 int int_input;

                 //Transformando a string em INT
                 while (true)
                 {
                     if (int.TryParse(input, out int_input)) { break; }
                     else
                     {
                         Console.WriteLine("Insira um numero!");
                     }
                 }

                 //Tela inicial de login ou registro (controlando os loops)
                 if (int_input == 1) { registrar(); }
                 else if (int_input == 2) { logar(); }
             }

             void interfaceUsuario()
             {
                 interf = true;
                 while (interf)
                 {
                     Console.Clear();

                     Console.WriteLine($"Seja bem-vindo a interface de usuario {profile.user}");
                     Console.WriteLine("O que o usuario gostaria de fazer?");
                     Console.WriteLine("1 - Transferencia");
                     Console.WriteLine("2 - Verificar saldo");
                     Console.WriteLine("3 - Verificar chaves de transferencia");
                     Console.WriteLine("4 - Fechar aplicativo (Tela de login)");
                     Console.WriteLine("5 - Logout (desconectar)");
                     Console.WriteLine("6 - Sair (Encerrar progama)");

                     string input = Console.ReadLine();

                     if (input == "1") { transferencia(); }

                     else if (input == "2") { verificaSaldo(); }

                     else if (input == "3") { verificaChave(); }

                     else if (input == "4")
                     {
                         Console.Clear();
                         logar(); ; interf = false;
                     }

                     else if (input == "5") 
                     {
                         interf = false;                        
                         registro = true;
                         inicio();

                     }

                     else if (input == "6")
                     {
                         Console.WriteLine("Tchau...");
                         Thread.Sleep(2000);
                         Environment.Exit(0);
                     }
                     else { Console.WriteLine("Insira um numero valido"); }

                 }
             }

             //Funcao para verificar se uma string é um numero e jogando para dentro da variavel INT
             static void verificationInput(string texto, int numero)
             {
                 while (true)
                 {
                     if (int.TryParse(texto, out numero)) { break; }
                     else
                     {
                         Console.WriteLine("Insira um numero!");
                     }

                 }
             }

             //Funcao para trasnferir dinheiro
             void transferencia()
             {
                //Criando variavel da qnt a transferir
                 int quantia;
                 bool controle = true;
                 string chave = "";
                int userTransf = 0;

                while (controle)
                 {
                     int x = profile.atualUser;
                     Console.Clear();

                    while (true)
                    {
                        Console.WriteLine("Insira a chave do usuario");
                        string input_1 = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input_1))
                        {
                            break;
                        }
                        else { Console.WriteLine("Ensira uma chave valida!"); }

                        for (int i = 0; i < profile.users.GetLength(0); i++)
                        {
                            if (profile.users[i, 2] == input_1)
                            {
                                chave = input_1;
                                userTransf = i;
                                break;
                            }
                            else if (profile.users[i, 3] == input_1)
                            {
                                chave = input_1;
                                userTransf = i;
                                break;
                            }
                            else if (profile.users[i, 4] == input_1)
                            {
                                chave = input_1;
                                userTransf = i;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Insira um valor valido.");
                            }
                        }
                    }
                    while (true)
                    {
                        //Verificando se é INT
                        Console.WriteLine("Insira a quantidade desejada");
                        string input = Console.ReadLine()!;
                        if (int.TryParse(input, out quantia)) { break; }
                        else
                        { Console.WriteLine("Insira um numero valido"); }
                    }


                    while (true)
                    {
                        //Subtraindo o dinheiro do saldo
                        if (quantia > profile.saldos[x, 0])
                        {
                            Console.WriteLine("Saldo insulficiente!");
                        }
                        else
                        {
                            profile.saldos[x, 0] -= quantia;
                            //Console.WriteLine($"Seu saldo atual é de: {profile.saldos[x, 0]}");

                            Console.WriteLine($"Deseja transferir {quantia} para o {profile.users[userTransf, 0]}. Digite sim para confimar e qualquer outra coisa para cancelar");
                            string input = Console.ReadLine();

                            if (!string.IsNullOrEmpty(input))
                            {
                                if(input.ToLower() == "sim") 
                                {
                                    profile.saldos[userTransf, 0] += quantia;
                                    Console.WriteLine($"Transferencia realizada com sucesso para o {profile.users[userTransf, 0]}");
                                    Console.WriteLine("Aperte enter para retornar a interface de usuario");
                                    Console.ReadLine();
                                    Console.Clear();
                                    controle = false;
                                    break;
                                }
                                else 
                                {
                                    Console.WriteLine("Aperte enter para retornar a interface de usuario");
                                    Console.ReadLine();
                                    Console.Clear();
                                    controle = false;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Digite um valor valido");
                            }

                        }

                    }
                }
             }

             //Funcao para verificar o saldo
             void verificaSaldo()
             {
                 int x = profile.atualUser;
                 Console.Clear();
                 Console.WriteLine("Ola, seu saldo atual é " + profile.saldos[x,0]);
                 Console.WriteLine("Aperte enter para retornar a interface de usuario");
                 Console.ReadLine();
                 Console.Clear();

             }

             //Funcao para verificar chaves
             void verificaChave() 
             {
                 bool controle = true;

                 while (controle)
                 {
                     Console.Clear();
                     int input_int;
                     int key;

                     for (int i = 0; i < 3; i++)
                     {
                         int t = i + 1;
                         Console.WriteLine($"No momento sua chave {t} é : {profile.users[profile.atualUser,(i+2)]}");
                     }

                     Console.WriteLine("Deseja modificar alguma? se sim digite o numero do espaco da chave, caso contrario digite não para voltar a interface de usuario. ( LIMITE DE 3 CHAVES )");
                     string input = Console.ReadLine()!;

                     if (input.ToLower() == "não" || input.ToLower() == "nao")
                     {

                         Thread.Sleep(2000);
                         Console.Clear();
                         controle = false;
                     }
                     else
                     { 
                         while (true)
                         {
                             if (int.TryParse(input, out input_int)) { break; }
                             else { Console.WriteLine("Insira um numero valido"); }
                         }

                         while (true)
                         {
                             key = input_int - 1;

                             if (key > 2) { Console.WriteLine("Insira um numero valido"); }
                             else 
                            { 
                                break;                             
                            }
                         }

                         Console.WriteLine("Insira a nova chave");
                        //profile.keyTransf[key] = Console.ReadLine();

                        if (key == 0) { key = key+ 2; } 
                        else if (key == 1) { key = key + 2; } 
                        else if (key == 2) { key = key + 2; }

                        profile.users[profile.atualUser, key] = Console.ReadLine();
                         Console.WriteLine("Chave adicionada com sucesso. Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        controle = false;
                     }
                 }
             }

            void adicionando() 
            {
                profile.numeroUsers += 1;
                int x = profile.numeroUsers - 1;

                profile.atualUser = x;

                profile.users[x, 0] = profile.user;
                profile.users[x, 1] = profile.senha;

                Console.WriteLine(profile.users[x, 0]);
                Console.WriteLine(profile.users[x, 1]);
                Console.WriteLine(profile.saldos[x, 0]);
            }
             

        }
    }

    public class User 
    {
        //Criando banco de dados dos usuarios com 10 linhas e 5 colunas ou seja esse banco de dados so suporta 10 usuarios
        // Usuario Senha chave1 chave2  chave3 
        // Usuario Senha chave1 chave2  chave3 
        // Usuario Senha chave1 chave2  chave3 
        // Usuario Senha chave1 chave2  chave3 
        // Usuario Senha chave1 chave2  chave3 
        public string[,] users = new string[10,5];

        public float[,] saldos = 
        {
            {1000}, 
            {1000}, 
            {1000}, 
            {1000}, 
            {1000},
            {1000}, 
            {1000}, 
            {1000}, 
            {1000}, 
            {1000},
            {1000},
        };

        public int numeroUsers;
        public int atualUser;

        public string user;
        public string senha;
    }
}   