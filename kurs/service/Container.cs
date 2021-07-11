using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using kurs.model;
using kurs.models;
using kurs.view;
using kurs.vm;

namespace kurs.service
{
    class Container
    {
        public static void ContainerMain()
        {
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DB>().As<IDB>();
            builderBase.RegisterType<ViewModelTables>().AsSelf();
            var containerBase = builderBase.Build();
            var viewmodelBase = containerBase.Resolve<ViewModelTables>();
            var viewBase = new Tables { DataContext = viewmodelBase };
            viewBase.Show();
        }

        public static ContainerBuilder ContainerZapas()
        {
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DB>().As<IDB>();
            builderBase.RegisterType<Zmodel>().AsSelf();
            return builderBase;
        }
        public static ContainerBuilder ContainerUOTPOTR()
        {
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DB>().As<IDB>();
            builderBase.Register((c, p) => new UOTPOTRmodel(p.Named<int>("p1"), p.Named<int>("p2")) ).AsSelf();
            return builderBase;
        }

        public static ContainerBuilder ContainerUOTGP()
        {
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DB>().As<IDB>();
            builderBase.Register((c, p) => new UOTGPmodel(p.Named<int>("p1"), p.Named<int>("p2"))).AsSelf();
            //builderBase.RegisterType<UOTGPmodel>().UsingConstructor(typeof(int), typeof(int)); - Просто чтобы в коструктор забить параметр
            return builderBase;
        }

        public static ContainerBuilder ContainerUOTGenerate()
        {
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DataTable>().AsSelf();
            //builderBase.RegisterType<DB>().As<IDB>().UsingConstructor(typeof(DataTable));

            builderBase.Register((c, p) => new DB(p.Named<DataTable>("p1"), p.Named<string>("p2"))).AsSelf().As<IDB>();
            builderBase.Register((c, p) => new DB(p.Named<DataTable>("p1"))).AsSelf().As<IDB>();

            return builderBase;
        }
    }
}
