export class App {
    router: any;

    configureRouter(config: any, router: any) {
        config.title = 'APV Gränichen';
        config.map([
            { route: ['', 'archive'], name: 'archive', moduleId: 'archive/archive', nav: true, title: 'Archiv' },
            { route: 'members', name: 'members', moduleId: 'members/members', nav: true, title: 'Mitglieder' },
            { route: 'archive/group', name: 'group', moduleId: 'archive/group/group', nav: false, title: 'Anlass' }
        ]);

        this.router = router;
    }
}
