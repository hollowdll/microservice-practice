use cli::cli::{
    Cli,
    Commands,
    CustomerCommands,
};
use clap::Parser;

fn main() {
    let cli = Cli::parse();

    match &cli.command {
        Some(Commands::Customer(args)) => {
            match &args.command {
                Some(CustomerCommands::Find(args)) => {
                    if args.all {
                        println!("Find all customers");
                    }
                },
                None => return,
            }
        },
        None => return,
    }
}
