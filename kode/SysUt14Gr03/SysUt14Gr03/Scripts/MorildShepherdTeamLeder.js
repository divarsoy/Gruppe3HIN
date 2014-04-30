(function () {
    var completeShepherd, init, setupShepherd;

    init = function () {
        return setupShepherd();
    };

    setupShepherd = function () {
        var shepherd;
        shepherd = new Shepherd.Tour({
            defaults: {
                classes: 'shepherd-element shepherd-open shepherd-theme-arrows'
            }
        });
        shepherd.addStep('RegistrerNyBruker', {
            title: 'Velkommen administrator :)',
            text: ['Her kan du registrere nye brukere', 'Du velger hvilken rettighet en bruker skal ha, skriver inn fornavn og etternavn og til slutt epost-adressen til vedkommede',
            'Når du har trykket på "Registrer bruker" knappen, vil vedkommede få en epost med en aktiveringslink han/hun må trykke på for å fullføre registreringen. '],
            attachTo: '.adm1 bottom',
            classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
            buttons: [
              {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next,
                  classes: 'shepherd-button-example-primary'
              }
            ]
        });
        shepherd.addStep('OversiktOverBrukere', {
            title: 'Oversikt over brukere!',
            text: ['En detaljert oversikt over brukere finner du her!.'],
            attachTo: '.adm2 bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });
        shepherd.addStep('AdministrereBrukere', {
            title: 'Administrere brukere!',
            text: ['Her har du mulighet til å endre informasjonen om hver enkelt bruker.',
                'Fornavn, Etternavn, Epost er mulig å endre, og du har i tillegg muligheten til å huke av en bruker som aktiv eller ei.',
                'Det er også mulig å sende en ny aktiveringslink dersom bruker av en eller annen grunn ikke mottatt den tidligere.'],
            attachTo: '.adm3 bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });
        shepherd.addStep('AdministrereRettigheter', {
            title: 'Administrere rettigheter!',
            text: ['Her har du mulighet til å legge til rettigeter.',
            'Det vil også være valg for å endre navn på rettigheter som allerede er opprettet.'],
            attachTo: '.adm4 bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });
        shepherd.addStep('EpostVarsler', {
            title: 'Epost varsler',
            text: ['Her kan du endre innstillinger på hvilke epost varsler du vil motta.',
            'Dette er slutten på omvisningen.',
            'Lykke til :)'],
            attachTo: '.adm5 bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Ferdig',
                  action: function () {
                      completeShepherd();
                      return shepherd.next();
                  }
              }
            ]
        });
        return shepherd.start();
    };

    completeShepherd = function () {
        return $('body').addClass('shepherd-completed');
    };

    $(init);

}).call(this);